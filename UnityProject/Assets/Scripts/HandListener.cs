using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;

public class HandListener : RunAbleThread
{
    public event Action<ELandMark, int, Vector3> onPointChange;
    public event Action<ELandMark, Vector3, Vector3, Vector3> depthCalculation;
    private int count = 0;

    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use

        using (PullSocket client = new PullSocket())
        {
            client.Connect("tcp://localhost:5555");


            Debug.Log("Listening to Hand");

            string message = null;
            bool gotMessage = false;
            while (Running)
            {
                //message = client.ReceiveFrameString(out gotMessage );
                gotMessage = client.TryReceiveFrameString(out message);
                if (gotMessage && message != "-1")
                {
                    var landmarks = JsonConvert.DeserializeObject<Dictionary<string, Vector3[]>>(message);
                    
                    var vectors = landmarks["right_hand"];
                    if (vectors.Length > 0)
                        depthCalculation.Invoke(ELandMark.RightHand, vectors[0], vectors[17], vectors[5]);

                    for (int i = 0; i < vectors.Length; i++)
                    {
                        onPointChange.Invoke(ELandMark.RightHand, i, vectors[i]);
                    }

                    vectors = landmarks["left_hand"];
                    if (vectors.Length > 0)
                        depthCalculation.Invoke(ELandMark.LeftHand, vectors[0], vectors[17], vectors[5]);
                    
                    for (int i = 0; i < vectors.Length; i++)
                    {
                        onPointChange.Invoke(ELandMark.LeftHand, i, vectors[i]);
                    }

                    //vectors = landmarks["pose"];
                    //for (int i = 0; i < vectors.Length; i++)
                    //{
                    //    onPointChange.Invoke(ELandMark.Pose, i, vectors[i]);
                    //}
                    count++;
                }
            }

        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use
    }
}

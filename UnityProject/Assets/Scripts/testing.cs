using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class testing : MonoBehaviour
{
    public bool run = false;
    [SerializeField] public Vector3[] myvectors;
    [SerializeField] public float[] myfloats;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            run = false;
            stringToJson();
        }
    }

    private void stringToJson()
    {

        //var json = JsonConvert.SerializeObject(mydata); // To Serialise
        //Debug.Log(json);

        string json = "[{\"x\":0.28302493691444397,\"y\":0.9196726679801941,\"z\":7.297759293578565e-05},{\"x\":0.3426106870174408,\"y\":0.9270083904266357,\"z\":-0.022085366770625114},{\"x\":0.38745585083961487,\"y\":0.9045205116271973,\"z\":-0.04491261765360832},{\"x\":0.4262344241142273,\"y\":0.873170793056488,\"z\":-0.06315770745277405},{\"x\":0.4664289355278015,\"y\":0.8493831753730774,\"z\":-0.08240953832864761},{\"x\":0.37300118803977966,\"y\":0.8035838603973389,\"z\":-0.05891904607415199},{\"x\":0.4035649001598358,\"y\":0.7257219552993774,\"z\":-0.08962230384349823},{\"x\":0.4242857098579407,\"y\":0.6758987903594971,\"z\":-0.10852909088134766},{\"x\":0.439971387386322,\"y\":0.632150411605835,\"z\":-0.1262197643518448},{\"x\":0.33924567699432373,\"y\":0.7870774865150452,\"z\":-0.06793151050806046},{\"x\":0.36263009905815125,\"y\":0.6960671544075012,\"z\":-0.10949444770812988},{\"x\":0.38180649280548096,\"y\":0.6314836740493774,\"z\":-0.1448977291584015},{\"x\":0.39625391364097595,\"y\":0.5715521574020386,\"z\":-0.1747041791677475},{\"x\":0.3016180098056793,\"y\":0.7898889780044556,\"z\":-0.07830430567264557},{\"x\":0.31354212760925293,\"y\":0.7015694379806519,\"z\":-0.1178077906370163},{\"x\":0.3275989592075348,\"y\":0.6389235854148865,\"z\":-0.1480216681957245},{\"x\":0.34092122316360474,\"y\":0.5820153951644897,\"z\":-0.17674800753593445},{\"x\":0.26286911964416504,\"y\":0.8079859018325806,\"z\":-0.08658242225646973},{\"x\":0.2499370574951172,\"y\":0.7426838874816895,\"z\":-0.11850915104150772},{\"x\":0.24196478724479675,\"y\":0.6960861086845398,\"z\":-0.14345969259738922},{\"x\":0.2362138032913208,\"y\":0.647309422492981,\"z\":-0.1645929217338562}]";
        var values = JsonConvert.DeserializeObject<Vector3[]>(json); // To Deserialise

        //var values = JsonUtility.FromJson<List<Vector3>>(json);
        Debug.Log(values);
        //Debug.Log(values.Length);
        Debug.Log(values.Length);


    }
}

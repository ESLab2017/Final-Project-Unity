using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player2Controller : MonoBehaviour {

    public Animator Player2Animator;
    public Player1Controller Player1;
    public Image WhiteCover;
    public Image Life1;
    public Image Life2;
    public Image Life3;
    public int HP;
    public bool isCatch;
    public float CatchPeriod;
    public float CatchCounter;
	public float ReadPeriod;
	public float ReadCounter;

    public SerialPort serialport2 = new SerialPort("COM4", 115200);
	//string distance;

	/*private void DataReceivedHandler(
		object sender,
		SerialDataReceivedEventArgs e)
	{
		//SerialPort sp = (SerialPort)sender;
		string distance = serialport2.ReadLine();
		Debug.Log(distance);
	}*/

    // Use this for initialization
    void Start()
    {
        serialport2.Open();
		HP = 3;
    }

    public void Attack()
    {
        // Debug.Log("Attack");
        DOTween.To(() => WhiteCover.color, (x) => WhiteCover.color = x, new Color(255, 255, 255, 255), 0.05f)
            .OnComplete(() => {
                DOTween.To(() => WhiteCover.color, (x) => WhiteCover.color = x, new Color(255, 255, 255, 0), 0.05f);
            });
        if (!Player1.isCatch)
        {
            Player1.Die();
        }
    }

    public void Catch()
    {
        // Debug.Log("Catch");
        // Player2Animator.enabled = false;
        isCatch = true;
        CatchCounter = CatchPeriod;
    }

    public void Die()
    {
		if (HP == 3) {
			Life3.enabled = false;
			HP -= 1;
		} else if (HP == 2) {
			Life2.enabled = false;
			HP -= 1;
		} else if (HP == 1) {
			Life1.enabled = false;
			HP -= 1;
		} else {
			HP--;
		}
        Player2Animator.SetTrigger("Die");
    }

    // Update is called once per frame
    void Update()
    {
		if (CatchCounter >= 0)
		{
			CatchCounter -= Time.deltaTime;
		}
		else
		{
			isCatch = false;
		}
		if (ReadCounter >= 0) {
			ReadCounter -= Time.deltaTime;
			return;
		}
		if (serialport2.IsOpen)
        {
            /*Debug.Log (serialport2.ReadLine ());
            //float.TryParse(serialport2.ReadLine(), out zacc);
            //string[] strs = line.Split(new string[]{","}, System.StringSplitOptions.RemoveEmptyEntries);
            //float knock=1;
            //float zacc;
            //float.TryParse(serialport2.ReadLine(), out knock);
            //float.TryParse (strs [1], out zacc);
            //Debug.Log(knock);
            //Debug.Log(zacc);*/
            
			string line = serialport2.ReadLine();
			// Debug.Log (line);
			if (line == "0") {
				Player2Animator.SetTrigger ("Catch");
				ReadCounter = ReadPeriod;
			}

        }
    }
}

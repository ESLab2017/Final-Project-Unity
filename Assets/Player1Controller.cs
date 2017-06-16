using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player1Controller : MonoBehaviour {

    public Animator Player1Animator;
    public Player2Controller Player2;
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
	public float temp;
	public float thresold;

    public SerialPort serialport1 = new SerialPort("COM3", 115200);
	private bool first=true;
	private int count;

    // Use this for initialization
    void Start () {
        serialport1.Open();
        HP = 3;
	}

    public void Attack()
    {
        Debug.Log("Attack");
        DOTween.To(() => WhiteCover.color, (x) => WhiteCover.color = x, new Color(255, 255, 255, 255), 0.05f)
            .OnComplete(()=> {
                DOTween.To(() => WhiteCover.color, (x) => WhiteCover.color = x, new Color(255, 255, 255, 0), 0.05f);
            });
		if (!Player2.isCatch) {
			Player2.Die ();
		} else {
			Die ();
		}
    }

    public void Catch()
    {
        Debug.Log("Catch");
    }

    public void Die()
    {
        if (HP == 3)
        {
            Life3.enabled = false;
            HP -= 1;
        }
        else if (HP == 2)
        {
            Life2.enabled = false;
            HP -= 1;
        }
        else if (HP == 1)
        {
            Life1.enabled = false;
            HP -= 1;
        }
        Player1Animator.SetTrigger("Die");
    }

    // Update is called once per frame
    void Update () {
		if (ReadCounter >= 0) {
			ReadCounter -= Time.deltaTime;
			return;
		}
        if (serialport1.IsOpen)
        {
			/*float zacc;
			//Debug.Log (serialport1.ReadLine ());
			string line = serialport1.ReadLine();
			float.TryParse(line, out zacc);
			serialport1.ReadTimeout = 25;
			float dif = temp - zacc;
			//Debug.Log(dif);
			//Debug.Log(zacc);
			temp = zacc;
			if (ReadCounter >= 0) {
				ReadCounter -= Time.deltaTime;
				//first = true;
				return;
			}
			if (first) {
				first = false;
			}
			else if (dif > thresold || dif<-thresold) {
				//Player1Animator.enabled = true;
				Player1Animator.SetTrigger("Attack");
				//ReadCounter = ReadPeriod;
			}*/

			string line = serialport1.ReadLine();
			serialport1.ReadTimeout = 125;
			// Debug.Log (line);
			if (line == "0") {
				Player1Animator.SetTrigger("Attack");
				ReadCounter = ReadPeriod;
			}       
		}
    }
}

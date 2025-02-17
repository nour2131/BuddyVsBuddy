﻿using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Incinerate : MonoBehaviour
{

    public float cooldownTime = 0.8f;
    private float nextFireTime = 0f;

    public bool duration = false;
    private float durationTime = 6f;
    public float incinerateDuration;
    private bool checkTalent = false;

    public bool isOnCd = false;
    private float cooldownTimer;

    private Image incinerateLoading;
    private Text incinerateCd;
    private Image fireOverlay;
    private Image incinerateBuff;
    private Text incinerateBuffDuration;

    private PhotonView photonView;



    private void Start()
    {
        incinerateLoading = GameObject.FindWithTag("IncImage").GetComponent<Image>();
        incinerateCd = GameObject.FindWithTag("IncText").GetComponent<Text>();
        fireOverlay = GameObject.FindWithTag("IncFire").GetComponent<Image>();
        incinerateBuff = GameObject.FindWithTag("IncBuff").GetComponent<Image>();
        incinerateBuffDuration = GameObject.FindWithTag("IncBuffText").GetComponent<Text>();
        incinerateDuration = durationTime;
        cooldownTimer = cooldownTime;
        fireOverlay.enabled = false;
        incinerateBuff.enabled = false;
        incinerateBuffDuration.text = "";
        photonView = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
          incinerateLoading.fillAmount = 0;
          if (incinerateDuration <= 0.0f)
          {
              nextFireTime = Time.time + cooldownTime;
              cooldownTimer = cooldownTime;
              incinerateDuration = durationTime;
              duration = false;
              fireOverlay.enabled = false;
              incinerateBuff.enabled = false;
              incinerateBuffDuration.text = "";
              Debug.Log(incinerateDuration);
          }
          if (Time.time > nextFireTime)
          {
              if (duration)
              {
                  incinerateDuration -= Time.deltaTime;
                  incinerateBuffDuration.text = $"{Mathf.RoundToInt(incinerateDuration)}";
              }
              if (Input.GetKeyDown(KeyCode.Alpha4))
              {
                  duration = true;
                  fireOverlay.enabled = true;
                  incinerateBuff.enabled = true;
              }
              isOnCd = false;
          }
          if (Time.time <= nextFireTime)
          {
              isOnCd = true;
  
          }
          if (isOnCd)
          {
              
              cooldownTimer -= Time.deltaTime;
              incinerateLoading.fillAmount = cooldownTimer / cooldownTime;
              incinerateCd.text = (cooldownTimer % cooldownTime).ToString();
              if (incinerateLoading.fillAmount <= 0.03f)
              {
                  incinerateCd.text = "";
              }
          }
  
          else
          {
              incinerateLoading.fillAmount = -1;
          }  
        }
    }
}

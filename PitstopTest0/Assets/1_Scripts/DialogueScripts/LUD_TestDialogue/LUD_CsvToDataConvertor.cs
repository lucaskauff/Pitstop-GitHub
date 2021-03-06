﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_CsvToDataConvertor : MonoBehaviour
    {
        
        //Private
        public TextAsset csvFile;
        string[] rows;

        //Public
        public List<NativeReaction> nativeReactionList = new List<NativeReaction>();




        void Awake()
        {
            //csvFile = Resources.Load<TextAsset>("LUD_CSV/combinaisons_depeceur_alpha_2");
            rows = csvFile.text.Split('\n');

            foreach (string row in rows)
            {
                //Debug.Log("row = " + row);

                nativeReactionList.Add(new NativeReaction(row));

                //Debug.Log(nativeReactionList[nativeReactionList.Count - 1].valueOfPlayerSentence);
            }

            //Debug.Log("nativeReactionList.Count = " + nativeReactionList.Count);
        }


    }
}
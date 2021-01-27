using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Project.Scripts
{
    [System.Serializable]
    public class SaveData
    {
        #region variables

        public float[] Pposition;
      
        public float[] Eposition;
        #endregion

        public SaveData(Player player)
        {
            //NEEDS UPDATING
            Pposition = new float[3];
            Pposition[0] = player.getRespawn().transform.position.x;
            Pposition[1] = player.getRespawn().transform.position.y;
            Pposition[2] = player.getRespawn().transform.position.z;

            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            for (int x = 0; x < 1; x++)
            {
                float[] EposSingle = new float[3];
                EposSingle[0] = enemyList[0].transform.position.x;
                EposSingle[1] = enemyList[0].transform.position.y;
                EposSingle[2] = enemyList[0].transform.position.z;
                Debug.Log("asda" + EposSingle[0] + EposSingle[1] + EposSingle[2]);
                Eposition = EposSingle;
                Debug.Log(x);
            }
        }

    }
}
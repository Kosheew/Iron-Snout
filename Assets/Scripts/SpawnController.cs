using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class SpawnController : MonoBehaviour, IService
    {
        [SerializeField] private int _counterEnemy;
        [SerializeField] private Transform _pointSpawnLeft, _pointSpawnRight;
        [SerializeField] private GameObject[] _prefabEnemy;
        [SerializeField] private List<GameObject> _poolEnemy;

        public void Init()
        {
            while (_counterEnemy > 0)
            {
                for (int i = 0; i < _prefabEnemy.Length; i++)
                {
                    GameObject prefaEnemy_left = Instantiate(_prefabEnemy[i], _pointSpawnLeft.position, Quaternion.identity, _pointSpawnLeft);
                    GameObject prefaEnemy_right = Instantiate(_prefabEnemy[i], _pointSpawnRight.position, Quaternion.identity, _pointSpawnRight);
                    _poolEnemy.Add(prefaEnemy_left);
                    _poolEnemy.Add(prefaEnemy_right);
                }
                _counterEnemy--;
            }
            foreach(var obj in _poolEnemy)
            {
                obj.SetActive(false);
            }
            StartCoroutine(ActiveEnemy());
        }

        IEnumerator ActiveEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                GameObject randomEnemy = null;

                while (randomEnemy == null || randomEnemy.activeSelf)
                {
                    int randomIndex = Random.Range(0, _poolEnemy.Count);
                    randomEnemy = _poolEnemy[randomIndex];
                }

                randomEnemy.SetActive(true);
            }
        }
    }
}
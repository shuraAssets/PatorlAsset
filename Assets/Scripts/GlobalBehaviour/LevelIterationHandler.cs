using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIterationHandler : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _particle;

    [SerializeField] private GameObject[] _levels;
    [SerializeField] private Transform[] _spawnPosition;

    private int _nextLevel = 0;

    #region Enable/Disable

    private void OnEnable()
    {
        ButtonsHandler.NextLevel += StartLastLevel;
    }

    private void OnDisable()
    {
        ButtonsHandler.NextLevel -= StartLastLevel;
    }

    #endregion


    private void Start()
    {
        _nextLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        StartLastLevel();
    }

    private void StartLastLevel()
    {

        if (_nextLevel + 1 > _levels.Length) _nextLevel = 0;

        PlayerPrefs.SetInt("CurrentLevel", _nextLevel);


        OffAllLevels();

        StartCoroutine(DelayPlayerSpawn());

        _levels[_nextLevel].SetActive(true);

        GlobalSettings.worldTime = 1;

        _nextLevel++;
    }

    private IEnumerator DelayPlayerSpawn()
    {
        _player.gameObject.SetActive(false);


        _particle.transform.position = new Vector3(_spawnPosition[_nextLevel].position.x, 0.5f, _spawnPosition[_nextLevel].position.z);
        _player.position = new Vector3(_spawnPosition[_nextLevel].position.x, 0.5f, _spawnPosition[_nextLevel].position.z);

        _particle.SetActive(true);

        yield return new WaitForSeconds(1f);

        _particle.SetActive(false);
        _player.gameObject.SetActive(true);
    }

    private void OffAllLevels()
    {
        for (var i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(false);
        }
    }
}

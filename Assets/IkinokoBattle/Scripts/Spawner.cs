using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Spawner : MonoBehaviour
{
    private static Spawner instance;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;
    public static Spawner Instance => instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Destroy(gameObject);
        return;
    }
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    /// <summary>敵出現のCoroutine</summary>
    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            //距離10のベクトル
            var distanceVector = new Vector3(10, 0);
            //プレイヤーの位置をベースにした敵の出現
            //y軸に対してベクトルをランダムに0度から360度回転させている
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector;
            //敵を出現させたい位置決定
            var spawnPos = playerStatus.transform.position + spawnPositionFromPlayer;
            //指定座標から一番近いNavMeshの座標を探す
            NavMeshHit navMeshHit;
            //NavMesh外に出ないようにするための処理
            if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 10, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }
            //10秒待つ
            yield return new WaitForSeconds(10);
            //プレイヤーが死んだらループを抜ける
            if(playerStatus.Life<=0)
            {
                break;
            }
        }
    }
}

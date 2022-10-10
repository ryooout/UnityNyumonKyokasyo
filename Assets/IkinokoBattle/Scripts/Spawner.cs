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
    /// <summary>�G�o����Coroutine</summary>
    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            //����10�̃x�N�g��
            var distanceVector = new Vector3(10, 0);
            //�v���C���[�̈ʒu���x�[�X�ɂ����G�̏o��
            //y���ɑ΂��ăx�N�g���������_����0�x����360�x��]�����Ă���
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360), 0) * distanceVector;
            //�G���o�����������ʒu����
            var spawnPos = playerStatus.transform.position + spawnPositionFromPlayer;
            //�w����W�����ԋ߂�NavMesh�̍��W��T��
            NavMeshHit navMeshHit;
            //NavMesh�O�ɏo�Ȃ��悤�ɂ��邽�߂̏���
            if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 10, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }
            //10�b�҂�
            yield return new WaitForSeconds(10);
            //�v���C���[�����񂾂烋�[�v�𔲂���
            if(playerStatus.Life<=0)
            {
                break;
            }
        }
    }
}

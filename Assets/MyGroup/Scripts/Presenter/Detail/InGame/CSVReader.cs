using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using Cysharp.Threading.Tasks;
using ISMS.Data;
namespace ISMS.Presenter.Detail.Stage
{
    public class CSVReader : IRepository
    {
        string[] StageDataFiles;    //�X�e�[�W��񂪏����ꂽ�t�@�C���̖��O
        readonly string FolderName = "/SmallOffice";    //�X�e�[�W�f�[�^�̂���t�H���_�[��
        readonly string stageNameFilePath = "/StageNames.txt";  //�X�e�[�W�f�[�^�̖��O�̈ꗗ�ƃt�@�C���p�X�������ꂽ�t�@�C��

        readonly ObjectDictionary _objDictionary;

        public CSVReader()
        {
            _objDictionary = new ObjectDictionary();
        }

        public async 

        async UniTask GetWebStageData()
        {
            await DownloadStageNameList(Application.streamingAssetsPath + FolderName + stageNameFilePath);  //�X�e�[�W�̖��O�̈ꗗ���擾
            
        }

        //StageDataFiles�ɃX�e�[�W�f�[�^�̖��O�̈ꗗ���Z�b�g
        async UniTask DownloadStageNameList(string thisURL)
        {
            UnityWebRequest DLData = UnityWebRequest.Get(thisURL);
            await DLData.SendWebRequest();

            // ���s��
            if (!string.IsNullOrEmpty(DLData.error))
            {
                Debug.Log("�擾���s");
            }
            else
            {
                List<string> stageNameList = new List<string>();
                StringReader reader = new StringReader(DLData.downloadHandler.text);
                // ��s���z��Ɋi�[����
                while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
                {
                    Debug.Log("�ǂݎ��");
                    string line = reader.ReadLine(); // ��s���ǂݍ���
                    stageNameList.Add(line);
                    //print(line);

                }

                StageDataFiles = stageNameList.ToArray();

            }

        }

        //���X�g���疼�O�w��ŃX�e�[�W�f�[�^���擾
        async UniTask DownloadStageData(string thisFileURL)
        {
            UnityWebRequest DLCsvData = UnityWebRequest.Get(thisFileURL);
            await DLCsvData.SendWebRequest();

            // ���s��
            if (!string.IsNullOrEmpty(DLCsvData.error))
            {
                Debug.Log("�擾���s:" + DLCsvData.error);
            }
            else
            {
                List<string[]> thisList = new List<string[]>();

                StringReader reader = new StringReader(DLCsvData.downloadHandler.text);

                // �J���}�ŕ�������s���ǂݍ���,���X�g�ɒǉ����Ă���
                while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
                {
                    Debug.Log("�ړ���ǂݎ��");
                    string line = reader.ReadLine(); // ��s���ǂݍ���
                    thisList.Add(line.Split(',')); // �J���}��؂�Ń��X�g�ǉ�
                }
                Debug.Log("�X�e�[�W�ǂݍ��݊���");

                SetObjData(thisList);
            }
        }

        void SetObjData(List<string[]> objData)
        {
            foreach(var data in objData)
            {
                var system = data[0];
                var obj = data[1];
                var describe = data[3];
                var explanation = data[4];
                var risk = int.Parse(data[2]);

                ISMS.Data.Object _obj = new ISMS.Data.Object(system, obj, describe, explanation, risk);
                _objDictionary.AddObject(system, _obj);
            }
        }

        public async UniTask<ObjectDictionary> GetObjectData(int StageNum)  //�w�肵���X�e�[�W�̃f�[�^��ǂݍ���Ńf�[�^�̃��X�g��Ԃ�
        {
            await DownloadStageData(Application.streamingAssetsPath + FolderName + StageDataFiles[StageNum]);  //�����Ŏw�肵���X�e�[�W�̃f�[�^���擾
            return _objDictionary;
        }


    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Days.Infra.Const;
using Scene = Days.Infra.Const.Scene;

namespace Days.Common
{
    public class SceneManager : MonoBehaviour
    {
        private void Start()
        {
            throw new NotImplementedException();
        }

        // 외부에서 호출되는 씬 변경 이벤트
        public void LoadScene(Scene scene)
        {
            
        }
        
        IEnumerator LoadSceneCoroutine(Scene scene)
        {
            yield return null;
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene);

            op.allowSceneActivation = false;
            float timer = 0.0f;
            while (!op.isDone)

            {
                yield return null;

                timer += Time.deltaTime;
                if (op.progress < 0.9f)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                    if (progressBar.fillAmount >= op.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                    if (progressBar.fillAmount == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }

    }
}
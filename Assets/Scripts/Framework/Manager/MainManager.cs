using UnityEngine;

namespace MetalMax
{
    public enum EnviromentMode
    {
        Developing,
        Test,
        Production,
    }

    public abstract class MainManager : MonoBehaviour
    {
        public EnviromentMode Mode;

        private static EnviromentMode mSharedMode;
        private static bool mModeSetted = false;

        void Start()
        {
            if (!mModeSetted)
            {
                mSharedMode = Mode;
                mModeSetted = true;
            }

            switch (mSharedMode)
            {
                case EnviromentMode.Developing:
                    LaunchInDevelopingMode();
                    break;
                case EnviromentMode.Test:
                    LaunchInTestMode();
                    break;
                case EnviromentMode.Production:
                    LaunchInProductionMode();
                    break;
            }
        }

        protected virtual void LaunchInDevelopingMode()
        {
            //创建空物体Managers，并挂载各种manager脚本
            GameObject newGo = Instantiate(Resources.Load<GameObject>("Prefab/Managers"));
            newGo.name = "Managers";
            DontDestroyOnLoad(newGo);

            GameManager.charactor.nameString = "测试";

            //置为true表示是新游戏
            GameManager.isNewGame = true;

            BaseGameController gameController = GetComponent<BaseGameController>();
            gameController.Init();
        }
        protected virtual void LaunchInTestMode()
        {

        }
        protected virtual void LaunchInProductionMode()
        {

        }
    }
}
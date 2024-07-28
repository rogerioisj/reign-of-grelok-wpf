using reign_of_grelok_wpf.infoModel;

namespace reign_of_grelok_wpf.stages
{
    interface IStage
    {
        public StageInfo LoadStageInfo(LoadStageAction backAction);
    }
}

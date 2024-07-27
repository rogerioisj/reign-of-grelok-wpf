using reign_of_grelok_wpf.infoModel;

namespace reign_of_grelok_wpf.stages
{
    delegate void CallbackStageMenu();
    delegate string ShowTextAction(string? key);
    delegate StageInfo LoadStageAction(LoadStageAction? action);
}

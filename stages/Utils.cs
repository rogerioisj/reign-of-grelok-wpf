using reign_of_grelok_wpf.infoModel;

namespace reign_of_grelok_wpf.stages
{
    /// <summary>
    /// Delegate to call a method to load a menu when the back button is pressed
    /// </summary>
    delegate void CallbackStageMenu();

    /// <summary>
    /// Delegate to call a function that shows a button text
    /// </summary>
    /// <param name="key"></param>
    /// <returns>The text to be presented</returns>
    delegate string ShowTextAction(string? key);

    /// <summary>
    /// Delegate to call a method that returns a StageInfo
    /// </summary>
    /// <param name="action"></param>
    /// <returns>The menu and stage description</returns>
    delegate StageInfo LoadStageAction(LoadStageAction? action);
}

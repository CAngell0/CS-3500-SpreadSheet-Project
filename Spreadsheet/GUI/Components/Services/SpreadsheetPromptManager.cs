using GUI.Components.Other.Prompt;
using GUI.Model;

namespace GUI.Components.Services;

public class SpreadsheetPromptManager {
    /// <summary> Constant for the default color of the prompts. Prompts get set to this color if one is not specified </summary>
    public static readonly BasicColorOption DEFAULT_PROMPT_COLOR = BasicColorOption.Blue;
    /// <summary> Duration in seconds that a prompt should last before its timer event is trigged. </summary>
    public static readonly int PROMPT_DURATION = 4;
    /// <summary> List of prompts that are currently active on the manager. </summary>
    public IReadOnlyList<PromptInfo> Prompts { get { return _prompts; } }
    /// <summary> Internal list of prompts that the public auto property references. This is to protect the data of the prompts. </summary>
    private List<PromptInfo> _prompts;
    /// <summary> Holds the metadata for each prompt associated with its UUID </summary>
    private Dictionary<Guid, PromptMetadata> _metadata;

    // private Thread _timerThread;
    
    /// <summary> Shared state object that updates the prompt panel when there has been a change to the prompts list. </summary>
    public PromptSharedState SharedState { get; private set; }

    public SpreadsheetPromptManager() {
        SharedState = new PromptSharedState();
        _prompts = new List<PromptInfo>();
        _metadata = new Dictionary<Guid, PromptMetadata>();
    }

    // private void UpdateTimers() {
    //     foreach (PromptInfo prompt in Prompts) {
    //         if (!_pausedTimers.Contains(prompt) && _promptPositions.TryGetValue(prompt, out int position)) {
    //             _promptTimers[position] = _promptTimers[position] - 1;
    //         }
    //     }
    // }

    /// <summary>
    ///     Adds a prompt to the manager. Does the same as <see cref="AddPrompt(string, string?, PromptButtonInfo[]?, BasicColorOption?)"/> but doesn't
    ///     require the DescriptionText parameter.
    /// </summary>
    public void AddPrompt(string headerText) => AddPrompt(headerText, null, null, null);

    /// <summary>
    ///     Adds a prompt to the manager. Does the same as <see cref="AddPrompt(string, string?, PromptButtonInfo[]?, BasicColorOption?)"/> but doesn't
    ///     require the Buttons parameter.
    /// </summary>
    public void AddPrompt(string headerText, string? descriptionText) => AddPrompt(headerText, descriptionText, null, null);

    /// <summary>
    ///     Adds a prompt to the manager. Does the same as <see cref="AddPrompt(string, string?, PromptButtonInfo[]?, BasicColorOption?)"/> but doesn't
    ///     require the Color parameter.
    /// </summary>
    public void AddPrompt(string headerText, string? descriptionText, PromptButtonInfo[]? buttons) => AddPrompt(headerText, descriptionText, buttons, null);

    /// <summary>
    ///     Adds a prompt to the manager. Also updates the UI in the PromptPanel component notifying its state
    ///     through the <see cref="PromptSharedState"/> manager.
    /// </summary>
    /// <param name="headerText"> Text inside the prompt header. <see cref="Prompt"/> </param>
    /// <param name="descriptionText"> Description inside the prompt for further information. <see cref="Prompt"/> </param>
    /// <param name="buttons"> List of button information objects that denote how the buttons should be made. <see cref="PromptButtonInfo"/> </param>
    /// <param name="color"> What color the prompt should be </param>
    public void AddPrompt(string headerText, string? descriptionText, PromptButtonInfo[]? buttons, BasicColorOption? color) {
        Guid uuid = Guid.NewGuid();
        _prompts.Add(new PromptInfo(headerText, descriptionText, buttons, color, uuid));
        _metadata.Add(uuid, new PromptMetadata(
            Prompts.Count - 1, 
            PROMPT_DURATION, 
            false,
            null
        ));

        SharedState.NotifyStateChanged();
    }

    
    /// <summary>
    ///     Does the same as <see cref="DeletePrompt(Guid)"/> but accepts a PromptInfo instance.
    ///     All it does is access its internal UUID property and pass it to the Guid version of
    ///     the method.
    /// </summary>
    public void DeletePrompt(PromptInfo promptInfo) => DeletePrompt(promptInfo.UUID);

    /// <summary>
    ///     Does the same as <see cref="DeletePrompt(Guid)"/> but accepts a Prompt component.
    ///     All it does is access its internal UUID property and pass it to the Guid version of
    ///     the method.
    /// </summary>
    public void DeletePrompt(Prompt promptComponent) => DeletePrompt(promptComponent.UUID);

    /// <summary>
    ///     Deletes a prompt from the manager. Also updates the UI in the PrompPanel by notifying its state
    ///     through the <see cref="PromptSharedState"/> manager.
    /// </summary>
    /// <param name="uuid"> Unqiue ID of the prompt that was given to it when the prompt was added </param>
    public void DeletePrompt(Guid uuid) {
        if (_metadata.TryGetValue(uuid, out PromptMetadata? metadata)) {
            _prompts.RemoveAt(metadata.ListPosition);
            _metadata.Remove(uuid);
        }

        SharedState.NotifyStateChanged();
    }



    public void RegisterPromptComponent(Prompt prompt){}
    
    /// <summary>
    ///     A model class that's used to keep track of a prompt's lifetime, its position in the backing
    ///     list, whether its timer is paused and the action it needs to run when the timer runs out for
    ///     that prompt.
    /// </summary>
    /// <param name="ListPosition"> Where the PromptInfo associated with this metadata is inside the _prompts list </param>
    /// <param name="Timer"> Keeps track of how much time the prompt has before the OnTimerRunout event is fired </param>
    /// <param name="Paused"> Whether or not the prompt timer is paused for this instance </param>
    /// <param name="OnTimerRunout"> Event to call when the timer runs out. This is usally set after the prompt has been created </param>
    private record PromptMetadata(int ListPosition, int Timer, bool Paused, Action? OnTimerRunout){}

    /// <summary>
    ///     A model class that stores the information relating to a prompt. This is used by the PromptPanel
    ///     component. Its properties are used to fill in the parameters of the Prompt component and create the 
    ///     actual UI elements on the page.
    /// </summary>
    /// <param name="HeaderText"> Text inside the prompt header. <see cref="Prompt"/> </param>
    /// <param name="DescriptionText"> Description inside the prompt for further information. <see cref="Prompt"/> </param>
    /// <param name="Buttons"> List of button information objects that denote how the buttons should be made. <see cref="PromptButtonInfo"/> </param>
    /// <param name="Color"> What color the prompt should be </param>
    /// <param name="UUID"> A unique ID to identify the prompt so it can have associated metadata in the manager </param>
    public record PromptInfo(string HeaderText, string? DescriptionText, PromptButtonInfo[]? Buttons, BasicColorOption? Color, Guid UUID) {
        public override int GetHashCode() {
            return UUID.GetHashCode();
        }
    }

    /// <summary>
    ///     A model class that stores the information related to a button inside the prompt header.
    /// </summary>
    /// <param name="ButtonText"> What the inside of the button should say (e.g. "Yes", "No", "Okay") </param>
    /// <param name="OnClick"> What the button should do when its clicked </param>
    public record PromptButtonInfo(string ButtonText, Action OnClick){}


    /// <summary>
    ///     This class is used to manage the state events on the prompt panel and any other components
    ///     that need to share state with the prompts.
    /// </summary>
    public class PromptSharedState {
        public event Action? OnChange;
        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}

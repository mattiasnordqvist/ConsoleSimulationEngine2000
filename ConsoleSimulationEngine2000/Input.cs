
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSimulationEngine2000
{
    public class Input
    {
        internal string LastInput { get; set; }
        internal string CurrentInput { get; set; } = "";
        internal string Suggestion { get; set; } = null;

        internal List<string> AutoCompleteWordList = new List<string>();
        internal List<string> suggestions = new List<string>();
        internal int currentSuggestion = -1;

        /// <summary>
        /// Sets the available auto complete suggestions.
        /// </summary>
        /// <param name="list"></param>
        public void SetAutoCompleteWordList(List<string> list)
        {
            AutoCompleteWordList = list;
        }

        /// <summary>
        /// Returns the last input and allows for new input.
        /// </summary>
        /// <returns></returns>
        public string Consume()
        {
            var returnValue = LastInput;
            LastInput = null;
            return returnValue;
        }

        /// <summary>
        /// True if there is any input
        /// </summary>
        /// <returns></returns>
        public bool HasInput => LastInput != null;

        internal void KeyInputted(ConsoleKeyInfo key)
        {
            if (!HasInput)
            {
                if (key.Key == ConsoleKey.Enter)
                {
                    if (Suggestion != null)
                    {
                        LastInput = Suggestion;
                    }
                    else
                    {
                        LastInput = CurrentInput;
                    }
                    CurrentInput = "";
                    Suggestion = null;
                    currentSuggestion = -1;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    CurrentInput = "";
                    Suggestion = null;
                    currentSuggestion = -1;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (CurrentInput.Length > 0)
                    {
                        CurrentInput = CurrentInput.Substring(0, CurrentInput.Length - 1);
                    }
                    Suggestion = null;
                    currentSuggestion = -1;
                }
                else if (key.Key == ConsoleKey.Tab)
                {
                    currentSuggestion++;
                    if (currentSuggestion > suggestions.Count - 1)
                    {
                        currentSuggestion = -1;
                        Suggestion = null;
                    }
                    else
                    {
                        Suggestion = suggestions[currentSuggestion];
                    }
                }
                else
                {
                    CurrentInput += key.KeyChar;
                    suggestions = AutoCompleteWordList.Where(x => x.StartsWith(CurrentInput)).ToList();
                    Suggestion = null;
                    currentSuggestion = -1;
                }
            }
        }
    }
}

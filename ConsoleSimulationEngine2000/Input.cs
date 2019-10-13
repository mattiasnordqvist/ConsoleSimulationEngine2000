using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSimulationEngine2000
{
    public class Input
    {
        internal Queue<string> LastInput { get; } = new Queue<string>();
        internal string CurrentInput { get; set; } = "";
        internal string Suggestion { get; set; } = null;

        internal List<string> AutoCompleteWordList = new List<string>();
        internal List<string> suggestions = new List<string>();
        internal int currentSuggestion = -1;

        public BaseDisplay CreateDisplay(int x, int y, int width, int height)
        {
            return new InputDisplay(this, x, y, width, height);
        }

        /// <summary>
        /// Sets the available auto complete suggestions.
        /// </summary>
        /// <param name="list"></param>
        public void SetAutoCompleteWordList(List<string> list)
        {
            AutoCompleteWordList = list;
        }

        /// <summary>
        /// Returns the the first input in the queue, if any. Throws exception if there is no input to consume.
        /// </summary>
        /// <returns></returns>
        public string Consume()
        {
            if (HasInput)
            {
                var returnValue = LastInput.Dequeue();
                return returnValue;
            }
            else
            {
                throw new InvalidOperationException("There's no input to consume");
            }
        }

        /// <summary>
        /// True if there is any input to consume
        /// </summary>
        /// <returns></returns>
        public bool HasInput => LastInput.TryPeek(out _);

        internal void KeyInputted(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Enter)
            {
                if (Suggestion != null)
                {
                    LastInput.Enqueue(Suggestion);
                }
                else
                {
                    LastInput.Enqueue(CurrentInput);
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

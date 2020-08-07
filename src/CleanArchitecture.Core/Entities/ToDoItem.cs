using System;
using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Entities
{
    public class ToDoItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public bool IsDone { get; private set; }

        public void MarkComplete()
        {
            IsDone = true;

            Events.Add(new ToDoItemCompletedEvent(this));
        }

        public override bool Equals(object obj) => Equals(obj as ToDoItem);

        public override int GetHashCode() => Id.GetHashCode();

        public bool Equals(ToDoItem todoItem) => todoItem?.Id == this.Id;

        public override string ToString()
        {
            string status = IsDone ? "Done!" : "Not done.";
            return $"{Id}: Status: {status} - {Title} - {Description}";
        }
    }
}

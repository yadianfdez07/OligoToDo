namespace Shared
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public int UserModelId { get; set; }
        public UserModel Owner { get; set; }

    }
}
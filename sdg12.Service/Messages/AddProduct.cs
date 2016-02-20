namespace sdg12.Service.Messages
{
    public class AddProductCommand
    {
        public AddProductCommand()
        {
        }

        public string ProductName { get; set; }
        public string ProductNotes { get; set; }
        public int UserId { get; set; }
    }

    public class AddProductResponse
    {
    }
}
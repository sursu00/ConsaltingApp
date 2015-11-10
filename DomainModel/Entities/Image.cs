namespace DomainModel.Entities
{
    public class Image
    {
        public Image(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }

        public Image()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}
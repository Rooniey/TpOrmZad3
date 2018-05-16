namespace TpORM.CustomBusinessModel
{
    public class MyProduct
    {
        public int ProductID { get; }
        public string Name { get; set; }
        public decimal StandardCost { get; set; }
        public string Color { get; set; }

        internal MyProduct(int id)
        {
            ProductID = id;
        }
    }
}
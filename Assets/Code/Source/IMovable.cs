namespace Code.Source
{
    public interface IMovable
    {
        protected float Speed { get; set; }
        public void Move();
    }
}
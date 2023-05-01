namespace MobilivaProject.Interfaces
{
    public interface IRabitMQProducer
    {
        public void SendOrderMessage<T>(T message);
    }

}

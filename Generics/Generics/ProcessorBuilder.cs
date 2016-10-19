namespace Generics
{
    public class ProcessorBuilder
    {
        public static ProcessorBuilder<TEngine> CreateEngine<TEngine>()
        {
            return new ProcessorBuilder<TEngine>();
        }

    }

    public class ProcessorBuilder<TEngine>
    {
        public ProcessorBuilder<TEngine, TEntity> For<TEntity>()
        {
            return new ProcessorBuilder<TEngine,TEntity>();
        }
    }

    public class ProcessorBuilder<TEngine, TEntity>
    {
        public Processor<TEngine, TEntity, TLogger> With<TLogger>()
        {
            return new Processor<TEngine, TEntity, TLogger>();
        }
    }
}

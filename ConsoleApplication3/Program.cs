using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace ConsoleApplication3
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();

            var container = builder.Build();

            //new PrimeiroDebito().Execute();

            var primeiroDebito = container.Resolve<PrimeiroDebito>();
            primeiroDebito.Execute();
            
            //var segundoDebito = new PrimeiroDebito(msg => new Historico(msg, new SalvadorDeHistoricoStubado()));
            //segundoDebito.Execute();
        }
    }


    class PrimeiroDebito {
        private Func<string, Historico> historico;
        public PrimeiroDebito(Func<string, Historico> historico)
        {
            this.historico = historico;
        }

        public void Execute()
        {
            //new Historico("Salva isso!");
            historico("Salva isso!");
        }
    }

    class Historico {

        private ISalvadorDeHistorico _salvadorDeHistorico;

        public Historico(string mensagem, ISalvadorDeHistorico salvadorDeHisorico = null)
        {
            this._salvadorDeHistorico = salvadorDeHisorico ?? new SalvadorDeHistorico();
            this._salvadorDeHistorico.Salvar(mensagem);
        }
    }

    interface ISalvadorDeHistorico {
        void Salvar(string mensagem);
    }

    class SalvadorDeHistoricoStubado : ISalvadorDeHistorico
    {
        public void Salvar(string mensagem)
        {
            
        }
    }


    class SalvadorDeHistorico : ISalvadorDeHistorico {
        public void Salvar(string mensagem)
        {
            throw new Exception("Nao pode executar isso");
        }
    }

}

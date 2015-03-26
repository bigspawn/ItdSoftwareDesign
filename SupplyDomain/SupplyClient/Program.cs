﻿using Feonufry.CUI.Menu.Builders;
using SupplyClient.Actions;
using SupplyDomain;
using SupplyDomain.Api;
using SupplyDomain.DataAccess;
using SupplyDomain.Entities;

namespace SupplyClient 
{
   class Program 
   {
        static void Main(string[] args)
        {
            var deliveryRepository = new MemoryRepository<Delivery>();
            var contractsRepository = new MemoryRepository<Contract>();
            var itemsRepository = new MemoryRepository<Item>();
            var orderedItemsRepository = new MemoryRepository<OrderedItem>();

            var demoData = new DemoDataGenerator(itemsRepository, contractsRepository);
            demoData.Generate();

            var itemApi = new ItemApi(itemsRepository);
            var deliveryApi = new DeliveryApi(deliveryRepository, contractsRepository);
            var contractApi = new ContractApi(contractsRepository, orderedItemsRepository, itemsRepository, deliveryRepository);

            var contractAction = new ContractActions(contractApi, itemApi);
            var checkAction = new CheckContractsAction(contractApi, deliveryApi);
            var statusesAction = new StatusesAction(contractApi, deliveryApi);
            var archiveContractsAction = new ArchiveContractsAction(contractApi);

            new MenuBuilder()
                .Title("Снабжение")
                .Repeatable()
                .Submenu("Добавление контракта")
                    .Item("Введите данные для нового контракта", contractAction)
                    .Exit("Назад")
                    .End()
                .Item("Изменить состояния", statusesAction)
                .Item("Текущие контракты", checkAction)
                .Item("Архивные контракты", archiveContractsAction)
                .Exit("Закрыть").GetMenu().Run();
        }
    }
}

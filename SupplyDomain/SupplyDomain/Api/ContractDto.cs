﻿using System;
using System.Collections.Generic;
using SupplyDomain.Entities;

namespace SupplyDomain.Api
{
    public class ContractDto
    {
        public Guid Id { get; set; }

        public ContractDto()
        {
            OrderedItems = new List<OrderedItemDto>();
        }

        public ContractDto(string number, Period period)
            :this()
        {
            Number = number;
            Period = period;
        }

        public Period Period { get; set; }
        public string Number { get; set; }
        public List<OrderedItemDto> OrderedItems { get; private set; }

        public override string ToString()
        {
            return String.Format("Number: {0}\nStart Date: {1}\nMonth repetition: {2}\nClose Date: {3}",
                Number, Period.StartDate,Period.MonthRepetition, Period.CloseDate);
        }

        public static ContractDto FromContract(Contract contract)
        {
            return new ContractDto(contract.Number, contract.Period) { Id = contract.Id };
        }
    }
}
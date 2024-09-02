﻿namespace Chocolatier.Domain.RequestFilter
{
    public class GetIngredientPaginationRequest : BaseRequestFilter
    {
        public DateTime InitialExpiratonDate { get; set; }
        public DateTime FinalExpiratonDate { get; set; }
        public Guid IngredientTypeId { get; set; }

    }
}

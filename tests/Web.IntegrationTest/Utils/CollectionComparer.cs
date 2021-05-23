using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.IntegrationTest.Utils
{
    public static class CollectionComparer
    {
        public static bool Compare<TFirstEntity, TSecondEntity>(
            IReadOnlyCollection<TFirstEntity> firstCollection,
            IReadOnlyCollection<TSecondEntity> secondCollection,
            Func<TFirstEntity, TSecondEntity, bool> compareMembers)
        {
            if (firstCollection.Count != secondCollection.Count)
            {
                return false;
            }

            return firstCollection.All(firstCollectionMember => secondCollection.Any(secondCollectionMember => compareMembers(firstCollectionMember, secondCollectionMember)));
        }
    }
}
using System.Windows;
using System.Windows.Controls.Primitives;

using Prism.Regions;

namespace PrismMetroSample.Infrastructure.CustomerRegionAdapters
{
    public class UniformGridRegionAdapter : RegionAdapterBase<UniformGrid>
    {
        public UniformGridRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, UniformGrid regionTarget) => region.Views.CollectionChanged += (s, e) =>
                                                                                 {
                                                                                     if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                                                                                     {
                                                                                         foreach (FrameworkElement element in e.NewItems)
                                                                                         {
                                                                                             regionTarget.Children.Add(element);
                                                                                         }
                                                                                     }
                                                                                 };

        protected override IRegion CreateRegion() => new AllActiveRegion();
    }
}

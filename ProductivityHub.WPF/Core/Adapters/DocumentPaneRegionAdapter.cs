using AvalonDock.Layout;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.WPF.Core.Adapters
{
    class DocumentPaneRegionAdapter : RegionAdapterBase<LayoutDocumentPane>
    {
        public DocumentPaneRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
            
        }
        protected override void Adapt(IRegion region, LayoutDocumentPane regionTarget)
        {
            if (region == null) throw new ArgumentNullException(nameof(region));
            if (regionTarget == null) throw new ArgumentNullException(nameof(regionTarget));

            region.Views.CollectionChanged += ((s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddDocumentToDockingManager(region, regionTarget, view);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveDocumentFromDockingManager(region, regionTarget, view);
                    }
                }
            });
        }

        private static void RemoveDocumentFromDockingManager(IRegion region, LayoutDocumentPane regionTarget, object view)
        {

            var document = regionTarget.Children.FirstOrDefault(d => d.Content == view);
            if (document != null)
            {
                regionTarget.Children.Remove(document);
                document.Close();
            }
        }


        private static void AddDocumentToDockingManager(IRegion region, LayoutDocumentPane regionTarget, object view)
        {

            var document = new LayoutDocument
            {
                Content = view,
                Title = view.GetType().Name
            };
            document.Closed += (sender, args) =>
            {
                if (region.Views.Contains(view))
                {
                    region.Remove(view);
                }
            };
            regionTarget.Children.Add(document);
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}

using AvalonDock;
using AvalonDock.Layout;
using Prism.Regions;
using ProductivityHub.WPF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.WPF.Core.Adapters
{
    public class DockingManagerRegionAdapter : RegionAdapterBase<DockingManager>
    {
        public DockingManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, DockingManager regionTarget)
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

        private static void RemoveDocumentFromDockingManager(IRegion region, DockingManager regionTarget, object view)
        {
            if (view is IDocumentLayout documentLayout)
            {
                var document = regionTarget.Layout.Descendents().OfType<LayoutDocument>().FirstOrDefault(d => d.Content == view);
                if (document != null)
                {
                    var parent = document.Parent as LayoutDocumentPane;
                    parent?.Children.Remove(document);
                    document.Close();
                }
            }
            else if (view is IAnchorableLayout toolLayout)
            {
                var tool = regionTarget.Layout.Descendents().OfType<LayoutAnchorable>().FirstOrDefault(d => d.Content == view);
                if (tool != null)
                {
                    var parent = tool.Parent as LayoutDocumentPane;
                    parent?.Children.Remove(tool);
                    tool.Close();
                }
            }
        }


        private static void AddDocumentToDockingManager(IRegion region, DockingManager regionTarget, object view)
        {
            if (view is IDocumentLayout documentLayout)
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
                regionTarget.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault()?.Children.Add(document);
            }
            else if (view is IAnchorableLayout toolLayout)
            {
                var tool = new LayoutAnchorable
                {
                    Content = view,
                    Title = view.GetType().Name
                };
                tool.Closed += (sender, args) =>
                {
                    if (region.Views.Contains(view))
                    {
                        region.Remove(view);
                    }
                };
                regionTarget.Layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault()?.Children.Add(tool);
            }
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}

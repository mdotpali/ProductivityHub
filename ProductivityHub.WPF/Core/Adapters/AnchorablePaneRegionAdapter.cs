using AvalonDock.Layout;
using Prism.Regions;
using ProductivityHub.WPF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProductivityHub.WPF.Core.Adapters
{
    /// <summary>
    /// Adapter for integrating AvalonDock's LayoutAnchorablePane with Prism regions.
    /// </summary>
    public class AnchorablePaneRegionAdapter : RegionAdapterBase<LayoutAnchorablePane>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorablePaneRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The region behavior factory.</param>
        public AnchorablePaneRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts the region to the LayoutAnchorablePane.
        /// </summary>
        /// <param name="region">The region to adapt.</param>
        /// <param name="regionTarget">The LayoutAnchorablePane to adapt to.</param>
        protected override void Adapt(IRegion region, LayoutAnchorablePane regionTarget)
        {
            if (region == null) throw new ArgumentNullException(nameof(region));
            if (regionTarget == null) throw new ArgumentNullException(nameof(regionTarget));

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddAnchorableToPane(region, regionTarget, view);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveAnchorableFromPane(region, regionTarget, view);
                    }
                }
            };
        }

        /// <summary>
        /// Removes an anchorable from the LayoutAnchorablePane.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The LayoutAnchorablePane.</param>
        /// <param name="view">The view to remove.</param>
        private static void RemoveAnchorableFromPane(IRegion region, LayoutAnchorablePane regionTarget, object view)
        {
            var anchorable = regionTarget.Children.FirstOrDefault(d => d.Content == view);
            if (anchorable != null)
            {
                regionTarget.Children.Remove(anchorable);
                anchorable.Close();
            }
        }

        /// <summary>
        /// Adds an anchorable to the LayoutAnchorablePane.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The LayoutAnchorablePane.</param>
        /// <param name="view">The view to add.</param>
        private static void AddAnchorableToPane(IRegion region, LayoutAnchorablePane regionTarget, object view)
        {
            if (view is not FrameworkElement frameworkElement)
                return;

            var viewModel = frameworkElement.DataContext;
            string toolName = GetToolName(viewModel, view);

            var anchorable = new LayoutAnchorable
            {
                Content = view,
                Title = toolName,
                IsSelected = true
            };

            BindTitleProperty(viewModel, anchorable, nameof(IAnchorableLayout.ToolName));

            anchorable.Closed += (sender, args) =>
            {
                if (region.Views.Contains(view))
                {
                    region.Remove(view);
                }
            };

            regionTarget.Children.Add(anchorable);
        }

        /// <summary>
        /// Gets the tool name from the view model or uses the default name.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="view">The view.</param>
        /// <returns>The tool name.</returns>
        private static string GetToolName(object viewModel, object view)
        {
            if (view is IAnchorableLayout ancLayout)
                return ancLayout.ToolName;

            if (viewModel == null)
                return view.GetType().Name;

            var toolNameProperty = viewModel.GetType().GetProperty(nameof(IAnchorableLayout.ToolName));
            if (toolNameProperty == null)
                return view.GetType().Name;

            var toolNameValue = toolNameProperty.GetValue(viewModel) as string;
            return !string.IsNullOrEmpty(toolNameValue) ? toolNameValue : view.GetType().Name;
        }

        /// <summary>
        /// Binds the title property of the layout content to the view model property.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="layoutContent">The layout content.</param>
        /// <param name="propertyName">The property name.</param>
        private static void BindTitleProperty(object viewModel, LayoutContent layoutContent, string propertyName)
        {
            if (viewModel == null)
                return;

            var property = viewModel.GetType().GetProperty(propertyName);
            if (property == null)
                return;

            BindingOperations.SetBinding(
                layoutContent,
                LayoutContent.TitleProperty,
                new Binding(propertyName)
                {
                    Source = viewModel,
                    Mode = BindingMode.OneWay
                });
        }

        /// <summary>
        /// Creates a new region.
        /// </summary>
        /// <returns>A new instance of <see cref="AllActiveRegion"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}

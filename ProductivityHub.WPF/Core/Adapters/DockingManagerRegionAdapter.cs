using AvalonDock;
using AvalonDock.Layout;
using Prism.Regions;
using ProductivityHub.WPF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace ProductivityHub.WPF.Adapters
{
    /// <summary>
    /// Adapter for integrating AvalonDock's DockingManager with Prism regions.
    /// </summary>
    public class DockingManagerRegionAdapter : RegionAdapterBase<DockingManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DockingManagerRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The region behavior factory.</param>
        public DockingManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts the region to the DockingManager.
        /// </summary>
        /// <param name="region">The region to adapt.</param>
        /// <param name="regionTarget">The DockingManager to adapt to.</param>
        protected override void Adapt(IRegion region, DockingManager regionTarget)
        {
            if (region == null) throw new ArgumentNullException(nameof(region));
            if (regionTarget == null) throw new ArgumentNullException(nameof(regionTarget));

            region.Views.CollectionChanged += (s, e) =>
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
            };
        }

        /// <summary>
        /// Removes a document from the DockingManager.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The DockingManager.</param>
        /// <param name="view">The view to remove.</param>
        private static void RemoveDocumentFromDockingManager(IRegion region, DockingManager regionTarget, object view)
        {
            if (view is IDocumentLayout)
            {
                var document = regionTarget.Layout.Descendents().OfType<LayoutDocument>().FirstOrDefault(d => d.Content == view);
                if (document != null)
                {
                    var parent = document.Parent as LayoutDocumentPane;
                    parent?.Children.Remove(document);
                }
            }
            else if (view is IAnchorableLayout)
            {
                var tool = regionTarget.Layout.Descendents().OfType<LayoutAnchorable>().FirstOrDefault(d => d.Content == view);
                if (tool != null)
                {
                    var parent = tool.Parent as LayoutAnchorablePane;
                    parent?.Children.Remove(tool);
                }
            }
        }

        /// <summary>
        /// Adds a document to the DockingManager.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The DockingManager.</param>
        /// <param name="view">The view to add.</param>
        private static void AddDocumentToDockingManager(IRegion region, DockingManager regionTarget, object view)
        {
            if (view is not FrameworkElement frameworkElement)
                return;

            var viewModel = frameworkElement.DataContext;

            if (view is IDocumentLayout documentLayout)
            {
                AddLayoutDocument(region, regionTarget, view, viewModel, documentLayout);
            }
            else if (view is IAnchorableLayout toolLayout)
            {
                AddLayoutAnchorable(region, regionTarget, view, viewModel, toolLayout);
            }
        }

        /// <summary>
        /// Adds a LayoutDocument to the DockingManager.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The DockingManager.</param>
        /// <param name="view">The view to add.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="documentLayout">The document layout.</param>
        private static void AddLayoutDocument(IRegion region, DockingManager regionTarget, object view, object viewModel, IDocumentLayout documentLayout)
        {
            string documentName = GetDocumentName(viewModel, documentLayout.DocumentName);

            var document = new LayoutDocument
            {
                Content = view,
                Title = documentName,
                IsSelected = true
            };

            BindTitleProperty(viewModel, document, nameof(documentLayout.DocumentName));

            document.Closed += (sender, args) =>
            {
                if (region.Views.Contains(view))
                {
                    region.Remove(view);
                }
            };

            regionTarget.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault()?.Children.Add(document);
        }

        /// <summary>
        /// Adds a LayoutAnchorable to the DockingManager.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The DockingManager.</param>
        /// <param name="view">The view to add.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="toolLayout">The tool layout.</param>
        private static void AddLayoutAnchorable(IRegion region, DockingManager regionTarget, object view, object viewModel, IAnchorableLayout toolLayout)
        {
            string toolName = GetToolName(viewModel, toolLayout.ToolName);

            var tool = new LayoutAnchorable
            {
                Content = view,
                Title = toolName,
                IsSelected = true
            };

            BindTitleProperty(viewModel, tool, nameof(toolLayout.ToolName));

            tool.Closed += (sender, args) =>
            {
                if (region.Views.Contains(view))
                {
                    region.Remove(view);
                }
            };

            regionTarget.Layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault()?.Children.Add(tool);
        }

        /// <summary>
        /// Gets the document name from the view model or uses the default name.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="defaultName">The default name.</param>
        /// <returns>The document name.</returns>
        private static string GetDocumentName(object viewModel, string defaultName)
        {
            if (viewModel == null)
                return defaultName ?? "Document Panel";

            var documentNameProperty = viewModel.GetType().GetProperty(nameof(IDocumentLayout.DocumentName));
            if (documentNameProperty == null)
                return defaultName ?? "Document Panel";

            var documentNameValue = documentNameProperty.GetValue(viewModel) as string;
            return !string.IsNullOrEmpty(documentNameValue) ? documentNameValue : defaultName ?? "Document Panel";
        }

        /// <summary>
        /// Gets the tool name from the view model or uses the default name.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="defaultName">The default name.</param>
        /// <returns>The tool name.</returns>
        private static string GetToolName(object viewModel, string defaultName)
        {
            if (viewModel == null)
                return defaultName ?? "Tool Panel";

            var toolNameProperty = viewModel.GetType().GetProperty(nameof(IAnchorableLayout.ToolName));
            if (toolNameProperty == null)
                return defaultName ?? "Tool Panel";

            var toolNameValue = toolNameProperty.GetValue(viewModel) as string;
            return !string.IsNullOrEmpty(toolNameValue) ? toolNameValue : defaultName ?? "Tool Panel";
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

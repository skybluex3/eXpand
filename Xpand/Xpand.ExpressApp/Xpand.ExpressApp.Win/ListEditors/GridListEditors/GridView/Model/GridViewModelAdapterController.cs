using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;
using Xpand.ExpressApp.Win.ListEditors.GridListEditors.ColumnView.Model;
using Xpand.Persistent.Base.General.Model.Options;
using Xpand.Persistent.Base.ModelAdapter;

namespace Xpand.ExpressApp.Win.ListEditors.GridListEditors.GridView.Model {
    public class GridViewModelAdapterController : GridViewModelAdapterControllerBase {
        GridListEditor _gridListEditor;
        protected override void OnDeactivated() {
            base.OnDeactivated();
            if (_gridListEditor != null){
                _gridListEditor.ModelSaved-=GridListEditorOnModelSaved;
                _gridListEditor.ModelApplied-=GridListEditorOnModelApplied;
            }
        }

        protected override void OnActivated() {
            base.OnActivated();
            var listView = View as ListView;
            if (listView != null && listView.Editor != null && listView.Editor.GetType() == typeof(GridListEditor)) {
                _gridListEditor = (GridListEditor)listView.Editor;
                _gridListEditor.ModelSaved += GridListEditorOnModelSaved;
                _gridListEditor.ModelApplied += GridListEditorOnModelApplied;
            }
        }

        private void GridListEditorOnModelApplied(object sender, EventArgs eventArgs){
            new GridListEditorDynamicModelSynchronizer(_gridListEditor).ApplyModel();
        }

        private void GridListEditorOnModelSaved(object sender, EventArgs eventArgs){
            new GridListEditorDynamicModelSynchronizer(_gridListEditor).SynchronizeModel();
        }

        protected override void ExtendInterfaces(ModelInterfaceExtenders extenders) {
            extenders.Add<IModelListView, IModelListViewOptionsGridView>();
            extenders.Add<IModelColumn, IModelColumnOptionsGridView>();

            var builder = new InterfaceBuilder(extenders);
            var assembly = BuildAssembly(builder, typeof(XafGridView), typeof(GridColumn));


            builder.ExtendInteface<IModelOptionsGridView, XafGridView>(assembly);
            builder.ExtendInteface<IModelOptionsColumnGridView, GridColumn>(assembly);

            ExtendWithFont(extenders, builder, assembly);
        }

    }

}
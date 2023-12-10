using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace MAT.Web.UI
{
    /// <summary>
    /// A designer class for a strongly typed repeater <c>PaqueteRepeater</c>
    /// </summary>
	public class PaqueteRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PaqueteRepeaterDesigner"/> class.
        /// </summary>
		public PaqueteRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is PaqueteRepeater))
			{ 
				throw new ArgumentException("Component is not a PaqueteRepeater."); 
			} 
			base.Initialize(component); 
			base.SetViewFlags(ViewFlags.TemplateEditing, true); 
		}


		/// <summary>
		/// Generate HTML for the designer
		/// </summary>
		/// <returns>a string of design time HTML</returns>
		public override string GetDesignTimeHtml()
		{

			// Get the instance this designer applies to
			//
			PaqueteRepeater z = (PaqueteRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();

			//return z.RenderAtDesignTime();

			//	ControlCollection c = z.Controls;
			//Totem z = (Totem) Component;
			//Totem z = (Totem) Component;
			//return ("<div style='border: 1px gray dotted; background-color: lightgray'><b>TagStat :</b> zae |  qsdds</div>");

		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="PaqueteRepeater"/> Type.
    /// </summary>
	[Designer(typeof(PaqueteRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:PaqueteRepeater runat=\"server\"></{0}:PaqueteRepeater>")]
	public class PaqueteRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PaqueteRepeater"/> class.
        /// </summary>
		public PaqueteRepeater()
		{
		}

		/// <summary>
        /// Gets a <see cref="T:System.Web.UI.ControlCollection"></see> object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        /// <value></value>
        /// <returns>The collection of child controls for the specified server control.</returns>
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		private ITemplate m_headerTemplate;
		/// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(PaqueteItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate HeaderTemplate
		{
			get { return m_headerTemplate; }
			set { m_headerTemplate = value; }
		}

		private ITemplate m_itemTemplate;
		/// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(PaqueteItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate ItemTemplate
		{
			get { return m_itemTemplate; }
			set { m_itemTemplate = value; }
		}

		private ITemplate m_seperatorTemplate;
        /// <summary>
        /// Gets or sets the Seperator Template
        /// </summary>
        [Browsable(false)]
        [TemplateContainer(typeof(PaqueteItem))]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ITemplate SeperatorTemplate
        {
            get { return m_seperatorTemplate; }
            set { m_seperatorTemplate = value; }
        }
			
		private ITemplate m_altenateItemTemplate;
        /// <summary>
        /// Gets or sets the alternating item template.
        /// </summary>
        /// <value>The alternating item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(PaqueteItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate AlternatingItemTemplate
		{
			get { return m_altenateItemTemplate; }
			set { m_altenateItemTemplate = value; }
		}

		private ITemplate m_footerTemplate;
        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>The footer template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(PaqueteItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//      {
//         if (ChildControlsCreated)
//         {
//            return;
//         }

//         Controls.Clear();

//         //Instantiate the Header template (if exists)
//         if (m_headerTemplate != null)
//         {
//            Control headerItem = new Control();
//            m_headerTemplate.InstantiateIn(headerItem);
//            Controls.Add(headerItem);
//         }

//         //Instantiate the Footer template (if exists)
//         if (m_footerTemplate != null)
//         {
//            Control footerItem = new Control();
//            m_footerTemplate.InstantiateIn(footerItem);
//            Controls.Add(footerItem);
//         }
//
//         ChildControlsCreated = true;
//      }
	
		/// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            
        }

        /// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
                
        }		
		
		/// <summary>
      	/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
      	/// </summary>
		protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
      	{
         int pos = 0;

         if (dataBinding)
         {
            //Instantiate the Header template (if exists)
            if (m_headerTemplate != null)
            {
                Control headerItem = new Control();
                m_headerTemplate.InstantiateIn(headerItem);
                Controls.Add(headerItem);
            }
			if (dataSource != null)
			{
				foreach (object o in dataSource)
				{
						MAT.Entities.Paquete entity = o as MAT.Entities.Paquete;
						PaqueteItem container = new PaqueteItem(entity);
	
						if (m_itemTemplate != null && (pos % 2) == 0)
						{
							m_itemTemplate.InstantiateIn(container);
							
							if (m_seperatorTemplate != null)
							{
								m_seperatorTemplate.InstantiateIn(container);
							}
						}
						else
						{
							if (m_altenateItemTemplate != null)
							{
								m_altenateItemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
								
							}
							else if (m_itemTemplate != null)
							{
								m_itemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
							}
							else
							{
								// no template !!!
							}
						}
						Controls.Add(container);
						
						container.DataBind();
						
						pos++;
				}
			}
            //Instantiate the Footer template (if exists)
            if (m_footerTemplate != null)
            {
                Control footerItem = new Control();
                m_footerTemplate.InstantiateIn(footerItem);
                Controls.Add(footerItem);
            }

		}
			
			return pos;
		}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#region Design time
        /// <summary>
        /// Renders at design time.
        /// </summary>
        /// <returns>a  string of the Designed HTML</returns>
		internal string RenderAtDesignTime()
		{			
			return "Designer currently not implemented"; 
		}

		#endregion
	}

    /// <summary>
    /// A wrapper type for the entity
    /// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class PaqueteItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.Paquete _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PaqueteItem"/> class.
        /// </summary>
		public PaqueteItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PaqueteItem"/> class.
        /// </summary>
		public PaqueteItem(MAT.Entities.Paquete entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the PaqueteId
        /// </summary>
        /// <value>The PaqueteId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid PaqueteId
		{
			get { return _entity.PaqueteId; }
		}
        /// <summary>
        /// Gets the Descripcion
        /// </summary>
        /// <value>The Descripcion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Descripcion
		{
			get { return _entity.Descripcion; }
		}
        /// <summary>
        /// Gets the PrecioCama
        /// </summary>
        /// <value>The PrecioCama.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? PrecioCama
		{
			get { return _entity.PrecioCama; }
		}
        /// <summary>
        /// Gets the Moneda
        /// </summary>
        /// <value>The Moneda.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Moneda
		{
			get { return _entity.Moneda; }
		}
        /// <summary>
        /// Gets the Iva
        /// </summary>
        /// <value>The Iva.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Iva
		{
			get { return _entity.Iva; }
		}
        /// <summary>
        /// Gets the Alicuota
        /// </summary>
        /// <value>The Alicuota.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Alicuota
		{
			get { return _entity.Alicuota; }
		}
        /// <summary>
        /// Gets the Temporada
        /// </summary>
        /// <value>The Temporada.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Temporada
		{
			get { return _entity.Temporada; }
		}
        /// <summary>
        /// Gets the Cotizacion
        /// </summary>
        /// <value>The Cotizacion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? Cotizacion
		{
			get { return _entity.Cotizacion; }
		}
        /// <summary>
        /// Gets the Codigo
        /// </summary>
        /// <value>The Codigo.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Codigo
		{
			get { return _entity.Codigo; }
		}
        /// <summary>
        /// Gets the DestinoId
        /// </summary>
        /// <value>The DestinoId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 DestinoId
		{
			get { return _entity.DestinoId; }
		}
        /// <summary>
        /// Gets the PrecioSemiCama
        /// </summary>
        /// <value>The PrecioSemiCama.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? PrecioSemiCama
		{
			get { return _entity.PrecioSemiCama; }
		}
        /// <summary>
        /// Gets the Foto
        /// </summary>
        /// <value>The Foto.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Foto
		{
			get { return _entity.Foto; }
		}
        /// <summary>
        /// Gets the ServiciosParticulares
        /// </summary>
        /// <value>The ServiciosParticulares.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String ServiciosParticulares
		{
			get { return _entity.ServiciosParticulares; }
		}
        /// <summary>
        /// Gets the FechaCreacion
        /// </summary>
        /// <value>The FechaCreacion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FechaCreacion
		{
			get { return _entity.FechaCreacion; }
		}
        /// <summary>
        /// Gets the PublicWeb
        /// </summary>
        /// <value>The PublicWeb.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean PublicWeb
		{
			get { return _entity.PublicWeb; }
		}
        /// <summary>
        /// Gets the LastUpdate
        /// </summary>
        /// <value>The LastUpdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime LastUpdate
		{
			get { return _entity.LastUpdate; }
		}
        /// <summary>
        /// Gets the ModePublicity
        /// </summary>
        /// <value>The ModePublicity.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? ModePublicity
		{
			get { return _entity.ModePublicity; }
		}

        /// <summary>
        /// Gets a <see cref="T:MAT.Entities.Paquete"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MAT.Entities.Paquete Entity
        {
            get { return _entity; }
        }
	}
}

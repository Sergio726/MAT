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
    /// A designer class for a strongly typed repeater <c>HotelRepeater</c>
    /// </summary>
	public class HotelRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:HotelRepeaterDesigner"/> class.
        /// </summary>
		public HotelRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is HotelRepeater))
			{ 
				throw new ArgumentException("Component is not a HotelRepeater."); 
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
			HotelRepeater z = (HotelRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="HotelRepeater"/> Type.
    /// </summary>
	[Designer(typeof(HotelRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:HotelRepeater runat=\"server\"></{0}:HotelRepeater>")]
	public class HotelRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:HotelRepeater"/> class.
        /// </summary>
		public HotelRepeater()
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
		[TemplateContainer(typeof(HotelItem))]
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
		[TemplateContainer(typeof(HotelItem))]
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
        [TemplateContainer(typeof(HotelItem))]
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
		[TemplateContainer(typeof(HotelItem))]
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
		[TemplateContainer(typeof(HotelItem))]
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
						MAT.Entities.Hotel entity = o as MAT.Entities.Hotel;
						HotelItem container = new HotelItem(entity);
	
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
	public class HotelItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.Hotel _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HotelItem"/> class.
        /// </summary>
		public HotelItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HotelItem"/> class.
        /// </summary>
		public HotelItem(MAT.Entities.Hotel entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the HotelId
        /// </summary>
        /// <value>The HotelId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid HotelId
		{
			get { return _entity.HotelId; }
		}
        /// <summary>
        /// Gets the Nombre
        /// </summary>
        /// <value>The Nombre.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Nombre
		{
			get { return _entity.Nombre; }
		}
        /// <summary>
        /// Gets the Direccion
        /// </summary>
        /// <value>The Direccion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Direccion
		{
			get { return _entity.Direccion; }
		}
        /// <summary>
        /// Gets the Cp
        /// </summary>
        /// <value>The Cp.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Cp
		{
			get { return _entity.Cp; }
		}
        /// <summary>
        /// Gets the Telefono
        /// </summary>
        /// <value>The Telefono.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Telefono
		{
			get { return _entity.Telefono; }
		}
        /// <summary>
        /// Gets the Email
        /// </summary>
        /// <value>The Email.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Email
		{
			get { return _entity.Email; }
		}
        /// <summary>
        /// Gets the Contacto
        /// </summary>
        /// <value>The Contacto.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Contacto
		{
			get { return _entity.Contacto; }
		}
        /// <summary>
        /// Gets the CantidadHabitaciones
        /// </summary>
        /// <value>The CantidadHabitaciones.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? CantidadHabitaciones
		{
			get { return _entity.CantidadHabitaciones; }
		}
        /// <summary>
        /// Gets the Categoria
        /// </summary>
        /// <value>The Categoria.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Categoria
		{
			get { return _entity.Categoria; }
		}
        /// <summary>
        /// Gets the Child1
        /// </summary>
        /// <value>The Child1.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Child1
		{
			get { return _entity.Child1; }
		}
        /// <summary>
        /// Gets the Child2
        /// </summary>
        /// <value>The Child2.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Child2
		{
			get { return _entity.Child2; }
		}
        /// <summary>
        /// Gets the ChildHabitacion
        /// </summary>
        /// <value>The ChildHabitacion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ChildHabitacion
		{
			get { return _entity.ChildHabitacion; }
		}
        /// <summary>
        /// Gets the CheckIn
        /// </summary>
        /// <value>The CheckIn.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CheckIn
		{
			get { return _entity.CheckIn; }
		}
        /// <summary>
        /// Gets the CheckOut
        /// </summary>
        /// <value>The CheckOut.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CheckOut
		{
			get { return _entity.CheckOut; }
		}
        /// <summary>
        /// Gets the GoogleMapHtml
        /// </summary>
        /// <value>The GoogleMapHtml.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String GoogleMapHtml
		{
			get { return _entity.GoogleMapHtml; }
		}
        /// <summary>
        /// Gets the LocalidadId
        /// </summary>
        /// <value>The LocalidadId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? LocalidadId
		{
			get { return _entity.LocalidadId; }
		}

        /// <summary>
        /// Gets a <see cref="T:MAT.Entities.Hotel"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MAT.Entities.Hotel Entity
        {
            get { return _entity; }
        }
	}
}

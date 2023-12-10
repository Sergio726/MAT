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
    /// A designer class for a strongly typed repeater <c>PersonaClienteRepeater</c>
    /// </summary>
	public class PersonaClienteRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaClienteRepeaterDesigner"/> class.
        /// </summary>
		public PersonaClienteRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is PersonaClienteRepeater))
			{ 
				throw new ArgumentException("Component is not a PersonaClienteRepeater."); 
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
			PersonaClienteRepeater z = (PersonaClienteRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();
		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="PersonaClienteRepeater"/> Type.
    /// </summary>
	[Designer(typeof(PersonaClienteRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:PersonaClienteRepeater runat=\"server\"></{0}:PersonaClienteRepeater>")]
	public class PersonaClienteRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaClienteRepeater"/> class.
        /// </summary>
		public PersonaClienteRepeater()
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
		[TemplateContainer(typeof(PersonaClienteItem))]
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
		[TemplateContainer(typeof(PersonaClienteItem))]
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
        [TemplateContainer(typeof(PersonaClienteItem))]
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
		[TemplateContainer(typeof(PersonaClienteItem))]
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
		[TemplateContainer(typeof(PersonaClienteItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}
		
		
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

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//		{
//			if (ChildControlsCreated)
//			{
//				return;
//			}
//			Controls.Clear();
//
//			if (m_headerTemplate != null)
//			{
//				Control headerItem = new Control();
//				m_headerTemplate.InstantiateIn(headerItem);
//				Controls.Add(headerItem);
//			}
//
//			
//			if (m_footerTemplate != null)
//			{
//				Control footerItem = new Control();
//				m_footerTemplate.InstantiateIn(footerItem);
//				Controls.Add(footerItem);
//			}
//			ChildControlsCreated = true;
//		}
		
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
						MAT.Entities.PersonaCliente entity = o as MAT.Entities.PersonaCliente;
						PersonaClienteItem container = new PersonaClienteItem(entity);
	
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
	public class PersonaClienteItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.PersonaCliente _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaClienteItem"/> class.
        /// </summary>
		public PersonaClienteItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaClienteItem"/> class.
        /// </summary>
		public PersonaClienteItem(MAT.Entities.PersonaCliente entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the PersonaId
        /// </summary>
        /// <value>The PersonaId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid PersonaId
		{
			get { return _entity.PersonaId; }
		}
        /// <summary>
        /// Gets the Apellido
        /// </summary>
        /// <value>The Apellido.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Apellido
		{
			get { return _entity.Apellido; }
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
        /// Gets the NroDocumento
        /// </summary>
        /// <value>The NroDocumento.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String NroDocumento
		{
			get { return _entity.NroDocumento; }
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
        /// Gets the FechaNacimiento
        /// </summary>
        /// <value>The FechaNacimiento.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FechaNacimiento
		{
			get { return _entity.FechaNacimiento; }
		}
        /// <summary>
        /// Gets the Domicilio
        /// </summary>
        /// <value>The Domicilio.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Domicilio
		{
			get { return _entity.Domicilio; }
		}
        /// <summary>
        /// Gets the Sexo
        /// </summary>
        /// <value>The Sexo.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Sexo
		{
			get { return _entity.Sexo; }
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
        /// Gets the ClienteId
        /// </summary>
        /// <value>The ClienteId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid ClienteId
		{
			get { return _entity.ClienteId; }
		}
        /// <summary>
        /// Gets the RazonSocial
        /// </summary>
        /// <value>The RazonSocial.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String RazonSocial
		{
			get { return _entity.RazonSocial; }
		}
        /// <summary>
        /// Gets the Cuit
        /// </summary>
        /// <value>The Cuit.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Cuit
		{
			get { return _entity.Cuit; }
		}
        /// <summary>
        /// Gets the Moneda
        /// </summary>
        /// <value>The Moneda.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Moneda
		{
			get { return _entity.Moneda; }
		}
        /// <summary>
        /// Gets the Empresa
        /// </summary>
        /// <value>The Empresa.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Empresa
		{
			get { return _entity.Empresa; }
		}
        /// <summary>
        /// Gets the Ocupacion
        /// </summary>
        /// <value>The Ocupacion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Ocupacion
		{
			get { return _entity.Ocupacion; }
		}
        /// <summary>
        /// Gets the FormaPago
        /// </summary>
        /// <value>The FormaPago.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? FormaPago
		{
			get { return _entity.FormaPago; }
		}
        /// <summary>
        /// Gets the CondicionIva
        /// </summary>
        /// <value>The CondicionIva.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? CondicionIva
		{
			get { return _entity.CondicionIva; }
		}
        /// <summary>
        /// Gets the VendedorId
        /// </summary>
        /// <value>The VendedorId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? VendedorId
		{
			get { return _entity.VendedorId; }
		}
        /// <summary>
        /// Gets the Fax
        /// </summary>
        /// <value>The Fax.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Fax
		{
			get { return _entity.Fax; }
		}
        /// <summary>
        /// Gets the Web
        /// </summary>
        /// <value>The Web.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Web
		{
			get { return _entity.Web; }
		}
        /// <summary>
        /// Gets the Idioma
        /// </summary>
        /// <value>The Idioma.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Idioma
		{
			get { return _entity.Idioma; }
		}
        /// <summary>
        /// Gets the Promotor
        /// </summary>
        /// <value>The Promotor.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Promotor
		{
			get { return _entity.Promotor; }
		}
        /// <summary>
        /// Gets the Observacion
        /// </summary>
        /// <value>The Observacion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Observacion
		{
			get { return _entity.Observacion; }
		}
        /// <summary>
        /// Gets the TipoId
        /// </summary>
        /// <value>The TipoId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 TipoId
		{
			get { return _entity.TipoId; }
		}
        /// <summary>
        /// Gets the TipoDocumento
        /// </summary>
        /// <value>The TipoDocumento.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? TipoDocumento
		{
			get { return _entity.TipoDocumento; }
		}
        /// <summary>
        /// Gets the Celular
        /// </summary>
        /// <value>The Celular.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Celular
		{
			get { return _entity.Celular; }
		}
        /// <summary>
        /// Gets the Nacionalidad
        /// </summary>
        /// <value>The Nacionalidad.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Nacionalidad
		{
			get { return _entity.Nacionalidad; }
		}
        /// <summary>
        /// Gets the PaisResidencia
        /// </summary>
        /// <value>The PaisResidencia.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String PaisResidencia
		{
			get { return _entity.PaisResidencia; }
		}
        /// <summary>
        /// Gets the Provincia
        /// </summary>
        /// <value>The Provincia.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Provincia
		{
			get { return _entity.Provincia; }
		}

	}
}

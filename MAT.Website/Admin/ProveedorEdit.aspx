<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ProveedorEdit.aspx.cs" Inherits="ProveedorEdit" Title="Proveedor Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Proveedor - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="ProveedorId" runat="server" DataSourceID="ProveedorDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ProveedorFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ProveedorFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Proveedor not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ProveedorDataSource ID="ProveedorDataSource" runat="server"
			SelectMethod="GetByProveedorId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="ProveedorId" QueryStringField="ProveedorId" Type="String" />

			</Parameters>
		</data:ProveedorDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewServicio1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewServicio1_SelectedIndexChanged"			 			 
			DataSourceID="ServicioDataSource1"
			DataKeyNames="ServicioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Servicio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]" />				
				<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="[Precio]" />				
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]" />				
				<asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="[Iva]" />				
				<asp:BoundField DataField="Alicuota" HeaderText="Alicuota" SortExpression="[Alicuota]" />				
				<asp:BoundField DataField="Validez" HeaderText="Validez" SortExpression="[Validez]" />				
				<asp:BoundField DataField="VisibilidadTarifa" HeaderText="Visibilidad Tarifa" SortExpression="[VisibilidadTarifa]" />				
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="ProveedorEdit.aspx?ProveedorId={0}" DataNavigateUrlFields="ProveedorId" DataContainer="ProveedorIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Transporte Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="TransporteIdSource" DataTextField="NroCoche" />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="TipoServicio" HeaderText="Tipo Servicio" SortExpression="[TipoServicio]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Servicio Found! </b>
				<asp:HyperLink runat="server" ID="hypServicio" NavigateUrl="~/admin/ServicioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ServicioDataSource ID="ServicioDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ServicioProperty Name="Hotel"/> 
					<data:ServicioProperty Name="Proveedor"/> 
					<data:ServicioProperty Name="Transporte"/> 
					<%--<data:ServicioProperty Name="PaqueteServicioCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ServicioFilter  Column="ProveedorId" QueryStringField="ProveedorId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ServicioDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewExcursion2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewExcursion2_SelectedIndexChanged"			 			 
			DataSourceID="ExcursionDataSource2"
			DataKeyNames="ExcursionId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Excursion.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]" />				
				<asp:BoundField DataField="Costo" HeaderText="Costo" SortExpression="[Costo]" />				
				<asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="[Observaciones]" />				
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="ProveedorEdit.aspx?ProveedorId={0}" DataNavigateUrlFields="ProveedorId" DataContainer="ProveedorIdSource" DataTextField="RazonSocial" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Excursion Found! </b>
				<asp:HyperLink runat="server" ID="hypExcursion" NavigateUrl="~/admin/ExcursionEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ExcursionDataSource ID="ExcursionDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ExcursionProperty Name="Proveedor"/> 
					<%--<data:ExcursionProperty Name="PaqueteExcursionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ExcursionFilter  Column="ProveedorId" QueryStringField="ProveedorId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ExcursionDataSource>		
		
		<br />
		

</asp:Content>


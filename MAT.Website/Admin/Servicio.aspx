<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Servicio.aspx.cs" Inherits="Servicio" Title="Servicio List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Servicio List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ServicioDataSource"
				DataKeyNames="ServicioId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Servicio.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ServicioId" HeaderText="Servicio Id" SortExpression="[ServicioID]" ReadOnly="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
				<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="[Precio]"  />
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]"  />
				<asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="[Iva]"  />
				<asp:BoundField DataField="Alicuota" HeaderText="Alicuota" SortExpression="[Alicuota]"  />
				<asp:BoundField DataField="Validez" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Validez" SortExpression="[Validez]"  />
				<asp:BoundField DataField="VisibilidadTarifa" HeaderText="Visibilidad Tarifa" SortExpression="[VisibilidadTarifa]"  />
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="ProveedorEdit.aspx?ProveedorId={0}" DataNavigateUrlFields="ProveedorId" DataContainer="ProveedorIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Transporte Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="TransporteIdSource" DataTextField="NroCoche" />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="TipoServicio" HeaderText="Tipo Servicio" SortExpression="[TipoServicio]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Servicio Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnServicio" OnClientClick="javascript:location.href='ServicioEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ServicioDataSource ID="ServicioDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
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
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ServicioDataSource>
	    		
</asp:Content>




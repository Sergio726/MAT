<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Paquete.aspx.cs" Inherits="Paquete" Title="Paquete List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PaqueteDataSource"
				DataKeyNames="PaqueteId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Paquete.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PaqueteId" HeaderText="Paquete Id" SortExpression="[PaqueteID]" ReadOnly="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
				<asp:BoundField DataField="PrecioCama" HeaderText="Precio Cama" SortExpression="[PrecioCama]"  />
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]"  />
				<asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="[Iva]"  />
				<asp:BoundField DataField="Alicuota" HeaderText="Alicuota" SortExpression="[Alicuota]"  />
				<asp:BoundField DataField="Temporada" HeaderText="Temporada" SortExpression="[Temporada]"  />
				<asp:BoundField DataField="Cotizacion" HeaderText="Cotizacion" SortExpression="[Cotizacion]"  />
				<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="[Codigo]"  />
				<data:HyperLinkField HeaderText="Destino Id" DataNavigateUrlFormatString="LocalidadEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="DestinoIdSource" DataTextField="IdDepartamento" />
				<asp:BoundField DataField="PrecioSemiCama" HeaderText="Precio Semi Cama" SortExpression="[PrecioSemiCama]"  />
				<asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="[Foto]"  />
				<asp:BoundField DataField="ServiciosParticulares" HeaderText="Servicios Particulares" SortExpression="[ServiciosParticulares]"  />
				<asp:BoundField DataField="FechaCreacion" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Creacion" SortExpression="[FechaCreacion]"  />
				<data:BoundRadioButtonField DataField="PublicWeb" HeaderText="Public Web" SortExpression="[PublicWeb]"  />
				<asp:BoundField DataField="LastUpdate" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Last Update" SortExpression="[LastUpdate]"  />
				<data:BoundRadioButtonField DataField="ModePublicity" HeaderText="Mode Publicity" SortExpression="[ModePublicity]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPaquete" OnClientClick="javascript:location.href='PaqueteEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PaqueteDataSource ID="PaqueteDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteProperty Name="Localidad"/> 
					<%--<data:PaqueteProperty Name="PaquetePrecioCollection" />--%>
					<%--<data:PaqueteProperty Name="PaqueteAdicionalCollection" />--%>
					<%--<data:PaqueteProperty Name="PaqueteExcursionCollection" />--%>
					<%--<data:PaqueteProperty Name="ViajeCollection" />--%>
					<%--<data:PaqueteProperty Name="PaqueteServicioCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PaqueteDataSource>
	    		
</asp:Content>




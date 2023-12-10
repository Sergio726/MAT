<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Vendedor.aspx.cs" Inherits="Vendedor" Title="Vendedor List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Vendedor List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="VendedorDataSource"
				DataKeyNames="VendedorId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Vendedor.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="PersonaEdit.aspx?PersonaId={0}" DataNavigateUrlFields="PersonaId" DataContainer="VendedorIdSource" DataTextField="Apellido" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Vendedor Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnVendedor" OnClientClick="javascript:location.href='VendedorEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:VendedorDataSource ID="VendedorDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:VendedorProperty Name="Persona"/> 
					<%--<data:VendedorProperty Name="FacturaCollection" />--%>
					<%--<data:VendedorProperty Name="NotaCollection" />--%>
					<%--<data:VendedorProperty Name="PagoCollection" />--%>
					<%--<data:VendedorProperty Name="VoucherCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:VendedorDataSource>
	    		
</asp:Content>




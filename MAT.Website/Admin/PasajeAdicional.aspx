<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PasajeAdicional.aspx.cs" Inherits="PasajeAdicional" Title="PasajeAdicional List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pasaje Adicional List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PasajeAdicionalDataSource"
				DataKeyNames="PasajeAdicionalId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PasajeAdicional.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PasajeAdicionalId" HeaderText="Pasaje Adicional Id" SortExpression="[PasajeAdicionalID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Pasaje Id" DataNavigateUrlFormatString="PasajeEdit.aspx?PasajeId={0}" DataNavigateUrlFields="PasajeId" DataContainer="PasajeIdSource" DataTextField="FechaReserva" />
				<data:HyperLinkField HeaderText="Adicional Id" DataNavigateUrlFormatString="AdicionalEdit.aspx?AdicionalId={0}" DataNavigateUrlFields="AdicionalId" DataContainer="AdicionalIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No PasajeAdicional Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPasajeAdicional" OnClientClick="javascript:location.href='PasajeAdicionalEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PasajeAdicionalDataSource ID="PasajeAdicionalDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeAdicionalProperty Name="Adicional"/> 
					<data:PasajeAdicionalProperty Name="Pasaje"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PasajeAdicionalDataSource>
	    		
</asp:Content>




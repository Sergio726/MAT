<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Pasajero.aspx.cs" Inherits="Pasajero" Title="Pasajero List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pasajero List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PasajeroDataSource"
				DataKeyNames="PasajeroId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Pasajero.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<data:HyperLinkField HeaderText="Pasajero Id" DataNavigateUrlFormatString="PersonaEdit.aspx?PersonaId={0}" DataNavigateUrlFields="PersonaId" DataContainer="PasajeroIdSource" DataTextField="Apellido" />
				<asp:BoundField DataField="Pasaporte" HeaderText="Pasaporte" SortExpression="[Pasaporte]"  />
				<asp:BoundField DataField="VencimientoPasaporte" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Vencimiento Pasaporte" SortExpression="[VencimientoPasaporte]"  />
				<asp:BoundField DataField="EmisionPasaporte" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Emision Pasaporte" SortExpression="[EmisionPasaporte]"  />
				<asp:BoundField DataField="PaisOrigen" HeaderText="Pais Origen" SortExpression="[PaisOrigen]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pasajero Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPasajero" OnClientClick="javascript:location.href='PasajeroEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PasajeroDataSource ID="PasajeroDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeroProperty Name="Persona"/> 
					<%--<data:PasajeroProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PasajeroDataSource>
	    		
</asp:Content>




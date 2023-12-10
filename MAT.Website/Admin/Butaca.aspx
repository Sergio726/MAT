<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Butaca.aspx.cs" Inherits="Butaca" Title="Butaca List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Butaca List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ButacaDataSource"
				DataKeyNames="ButacaId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Butaca.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ButacaId" HeaderText="Butaca Id" SortExpression="[ButacaID]" ReadOnly="True" />
				<asp:BoundField DataField="NroButaca" HeaderText="Nro Butaca" SortExpression="[NroButaca]"  />
				<asp:BoundField DataField="Piso" HeaderText="Piso" SortExpression="[Piso]"  />
				<asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="[Ubicacion]"  />
				<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="[Tipo]"  />
				<data:HyperLinkField HeaderText="Transporte Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="TransporteIdSource" DataTextField="NroCoche" />
				<asp:BoundField DataField="Fila" HeaderText="Fila" SortExpression="[Fila]"  />
				<asp:BoundField DataField="Posicion" HeaderText="Posicion" SortExpression="[Posicion]"  />
				<asp:BoundField DataField="CodigoButaca" HeaderText="Codigo Butaca" SortExpression="[CodigoButaca]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Butaca Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnButaca" OnClientClick="javascript:location.href='ButacaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ButacaDataSource ID="ButacaDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ButacaProperty Name="Transporte"/> 
					<%--<data:ButacaProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ButacaDataSource>
	    		
</asp:Content>




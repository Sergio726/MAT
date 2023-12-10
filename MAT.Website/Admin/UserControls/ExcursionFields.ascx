<%@ Control Language="C#" ClassName="ExcursionFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataExcursionId" runat="server" Text="Excursion Id:" AssociatedControlID="dataExcursionId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataExcursionId" Value='<%# Bind("ExcursionId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="200"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescripcion" runat="server" Display="Dynamic" ControlToValidate="dataDescripcion" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCosto" runat="server" Text="Costo:" AssociatedControlID="dataCosto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCosto" Text='<%# Bind("Costo") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataCosto" runat="server" Display="Dynamic" ControlToValidate="dataCosto" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataObservaciones" runat="server" Text="Observaciones:" AssociatedControlID="dataObservaciones" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataObservaciones" Text='<%# Bind("Observaciones") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataProveedorId" runat="server" Text="Proveedor Id:" AssociatedControlID="dataProveedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataProveedorId" DataSourceID="ProveedorIdProveedorDataSource" DataTextField="RazonSocial" DataValueField="ProveedorId" SelectedValue='<%# Bind("ProveedorId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ProveedorDataSource ID="ProveedorIdProveedorDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>



<%@ Control Language="C#" ClassName="PaqueteExcursionFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteExcursionId" runat="server" Text="Paquete Excursion Id:" AssociatedControlID="dataPaqueteExcursionId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPaqueteExcursionId" Value='<%# Bind("PaqueteExcursionId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataExcursionId" runat="server" Text="Excursion Id:" AssociatedControlID="dataExcursionId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataExcursionId" DataSourceID="ExcursionIdExcursionDataSource" DataTextField="Descripcion" DataValueField="ExcursionId" SelectedValue='<%# Bind("ExcursionId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ExcursionDataSource ID="ExcursionIdExcursionDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteId" runat="server" Text="Paquete Id:" AssociatedControlID="dataPaqueteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPaqueteId" DataSourceID="PaqueteIdPaqueteDataSource" DataTextField="Descripcion" DataValueField="PaqueteId" SelectedValue='<%# Bind("PaqueteId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:PaqueteDataSource ID="PaqueteIdPaqueteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>



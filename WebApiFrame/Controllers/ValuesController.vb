Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ValuesController
    Inherits ApiController

    Private Shared response_ As List(Of ResponseItem) = New List(Of ResponseItem) From {
        New ResponseItem With {.Id = 1, .Text = "valor item 1"},
        New ResponseItem With {.Id = 2, .Text = "valor item 2"},
        New ResponseItem With {.Id = 3, .Text = "Valor WebApi .NET Framework 4.8"}
    }

    ' GET api/values
    Public Function GetValues() As IEnumerable(Of ResponseItem)
        Return response_
    End Function

    ' GET api/values/5
    Public Function GetValue(ByVal id As Integer) As IHttpActionResult

        Try
            Return Ok(response_.Where(Function(x) x.Id = id).First)
        Catch ex As Exception
            Return NotFound()
        End Try
    End Function

    ' POST api/values
    Public Function PostValue(<FromBody()> ByVal newItem As ResponseItem) As IHttpActionResult
        'HttpResponseMessage
        'If newItem Is Nothing OrElse String.IsNullOrWhiteSpace(newItem.Text) Then
        '    Return Request.CreateResponse(HttpStatusCode.BadRequest, "El objeto enviado no es válido.")
        'End If

        'Dim nextId As Integer = If(response_.Count = 0, 1, response_.Max(Function(x) x.Id) + 1)
        'newItem.Id = nextId
        'response_.Add(newItem)

        'Return Request.CreateResponse(HttpStatusCode.OK, "Registro exitoso con Id: " & newItem.Id)

        If newItem Is Nothing OrElse String.IsNullOrWhiteSpace(newItem.Text) Then
            Return BadRequest("El objeto enviado no es válido.")
        End If

        Dim nextId As Integer = If(response_.Count = 0, 1, response_.Max(Function(x) x.Id) + 1)
        newItem.Id = nextId
        response_.Add(newItem)

        Return Ok("Registro exitoso con Id: " & newItem.Id)
    End Function

    ' PUT api/values/5
    Public Function PutValue(ByVal id As Integer, <FromBody()> ByVal updatedItem As ResponseItem) As IHttpActionResult

        Dim existingItem = response_.FirstOrDefault(Function(x) x.Id = id)

        If existingItem Is Nothing Then
            Return NotFound()
        End If

        ' Actualizar los valores del item existente
        existingItem.Text = updatedItem.Text

        Return Ok(existingItem)
    End Function

    ' DELETE api/values/5
    Public Function DeleteValue(ByVal id As Integer) As IHttpActionResult

        Dim itemToDelete = response_.FirstOrDefault(Function(x) x.Id = id)

        If itemToDelete Is Nothing Then
            Return NotFound()
        End If

        response_.Remove(itemToDelete)

        Return Ok("Elemento con Id " & id & " eliminado correctamente.")
    End Function
End Class


Public Class ResponseItem
    Public Property Id As Integer
    Public Property Text As String
End Class

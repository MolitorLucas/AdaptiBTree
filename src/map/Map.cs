using Godot;
using Godot.Collections; // Necessário para 'Array'

public partial class MazeTileMap : TileMapLayer
{
    // O atributo [Export] expõe a variável no Editor da Godot, ótimo para depuração.
    [Export]
    private Array<Vector2I> emptyCells = new Array<Vector2I>();

    public override void _Ready()
    {
        FindEmptyCells();
    }

    // É uma boa prática encapsular a lógica principal em sua própria função.
    // Assim, você pode chamá-la novamente se o mapa mudar.
    public void FindEmptyCells()
    {
        emptyCells.Clear(); // Limpa a lista antes de preenchê-la novamente.
        Array<Vector2I> cells = GetUsedCells();

        foreach (Vector2I cell in cells)
        {
            TileData data = GetCellTileData(cell);

            // Garante que 'data' não é nulo antes de acessar seus métodos.
            if (data != null && data.GetCustomData("isEmpty").As<bool>())
            {
                // Add() é o equivalente a 'push_back' e é mais eficiente que Insert(0, ...).
                emptyCells.Add(cell);
            }
        }
    }

    // Retorna a posição global de uma célula vazia aleatória.
    // Retorna Vector2.Zero se nenhuma célula vazia estiver disponível.
    public Vector2 GetRandomEmptyCellPosition()
    {
        // CRÍTICO: Verifica se a coleção está vazia para evitar um erro em tempo de execução.
        if (emptyCells.Count == 0)
        {
            GD.PushWarning("Nenhuma célula vazia foi encontrada no MazeTileMap.");
            return Vector2.Zero; // Retorna uma posição padrão ou lide com o caso conforme necessário.
        }

        Vector2I randomCell = emptyCells.PickRandom();
        return ToGlobal(MapToLocal(randomCell));
    }
}
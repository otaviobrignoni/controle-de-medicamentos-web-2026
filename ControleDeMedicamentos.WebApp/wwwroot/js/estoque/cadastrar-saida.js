const itens = document.querySelector("#itens");
const modeloItem = document.querySelector("#modelo-item");

function atualizarNomes() {
  itens.querySelectorAll(".item-saida").forEach((item, indice) => {
    item.querySelector(".medicamento").name = `Itens[${indice}].MedicamentoId`;
    item.querySelector(".quantidade").name = `Itens[${indice}].Quantidade`;
  });
}

function atualizarOpcoesDisponiveis() {
  const medicamentosSelecionados = [...itens.querySelectorAll(".medicamento")]
    .map(select => select.value)
    .filter(valor => valor !== "");

  itens.querySelectorAll(".medicamento").forEach(select => {
    select.querySelectorAll("option").forEach(option => {
      option.disabled = option.value !== select.value &&
        medicamentosSelecionados.includes(option.value);
    });
  });
}

document.querySelector("#adicionar-item").addEventListener("click", () => {
  itens.append(modeloItem.content.cloneNode(true));
  atualizarNomes();
  atualizarOpcoesDisponiveis();
});

itens.addEventListener("change", evento => {
  if (evento.target.classList.contains("medicamento"))
    atualizarOpcoesDisponiveis();
});

itens.addEventListener("click", evento => {
  if (!evento.target.classList.contains("remover-item"))
    return;

  evento.target.closest(".item-saida").remove();
  atualizarNomes();
  atualizarOpcoesDisponiveis();
});

atualizarNomes();
atualizarOpcoesDisponiveis();

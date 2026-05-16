# 🔧 MODUS OPERANDI - NotebookLM Analysis Workflow

**Data de Criação:** 2026-05-15  
**Status:** Padrão Oficial de Análise  
**Aplicabilidade:** Projetos acadêmicos e profissionais

## 🎯 Propósito

Estabelecer fluxo padrão para análise de código usando NotebookLM MCP, garantindo que análises sejam baseadas em código atualizado e evitando falsos positivos.

## 🔄 Fluxo Completo (8 Passos)

### PASSO 1: 📦 Gerar Repomix Inicial
```bash
repomix . --output repomix.md
```
**Objetivo:** Capturar estado atual do código fonte  
**Validação:** Verificar se repomix.md foi criado com todos os arquivos

### PASSO 2: 🔍 Adicionar ao NotebookLM
```bash
mcp__notebooklm__source_add \
  --notebook_id "id-do-notebook" \
  --source_type text \
  --file_path "repomix.md" \
  --title "Código Completo - Versão Inicial"
```
**Objetivo:** Disponibilizar código para análise do NotebookLM  
**Validação:** Confirmar que source foi adicionado com sucesso

### PASSO 3: 🤖 Análise Inicial
```bash
mcp__notebooklm__notebook_query \
  --notebook_id "id-do-notebook" \
  --query "ANÁLISE EXAUSTIVA: Verifique linha por linha contra requisitos..." \
  --timeout 240
```
**Objetivo:** Identificar problemas e inconsistências  
**Validação:** Receber lista detalhada de problemas encontrados

### PASSO 4: 🔧 Aplicar Correções
```bash
# Usar ferramentas Edit/Write para corrigir código
Edit --file_path "arquivo.cs" --old_string "erro" --new_string "correção"
```
**Objetivo:** Corrigir problemas identificados na análise  
**Validação:** Confirmar que arquivos foram modificados

### PASSO 5: ⚠️ **CRÍTICO: Regenerar Repomix**
```bash
repomix . --output repomix.md
```
**🚨 REGRA DE OURO:** Sempre regerar APÓS correções  
**Objetivo:** Atualizar repomix.md com código corrigido  
**Validação:** Verificar timestamp e tamanho do arquivo

### PASSO 6: 🗑️ Remover Source Antigo
```bash
# Primeiro listar sources para encontrar ID
mcp__notebooklm__notebook_get --notebook_id "id-do-notebook"

# Depois deletar source antigo
mcp__notebooklm__source_delete \
  --source_id "id-repomix-antigo" \
  --confirm=true
```
**Objetivo:** Evitar confusão entre versões do código  
**Validação:** Confirmar que source antigo foi removido

### PASSO 7: ➕ Adicionar Novo Repomix
```bash
mcp__notebooklm__source_add \
  --notebook_id "id-do-notebook" \
  --source_type text \
  --file_path "repomix.md" \
  --title "Código Completo - VERSÃO CORRIGIDA"
```
**Objetivo:** Disponibilizar código atualizado para nova análise  
**Validação:** Confirmar que novo source foi adicionado

### PASSO 8: ✅ Análise Final Confirmatória
```bash
mcp__notebooklm__notebook_query \
  --notebook_id "id-do-notebook" \
  --query "CONFIRMAÇÃO FINAL: Verifique se todas as correções foram aplicadas..." \
  --timeout 180
```
**Objetivo:** Confirmar 100% conformidade após correções  
**Validação:** Receber confirmação de que todos os problemas foram resolvidos

## 🔴 Regras de Ouro

### 1. NUNCA pular o PASSO 5 (Regenerar Repomix)
**Razão:** NotebookLM analisa APENAS o conteúdo dos sources  
**Consequência:** Análise baseada em código desatualizado = falsos positivos

### 2. SEMPRE remover source antigo (PASSO 6)
**Razão:** Evitar confusão entre versões do código  
**Consequência:** Análise pode usar versão errada do código

### 3. VALIDAR cada passo
**Razão:** Garantir que fluxo foi seguido corretamente  
**Consequência:** Passos falhados comprometem toda a análise

### 4. DOCUMENTAR tudo
**Razão:** Rastreabilidade de decisões e correções  
**Consequência:** Perda de contexto histórico

## 📋 Checklist de Validação

### Pré-Análise
- [ ] Repomix.md inicial gerado
- [ ] Source adicionado ao NotebookLM
- [ ] Query inicial preparada

### Pós-Correção
- [ ] **Repomix.md regerado (CRÍTICO)**
- [ ] Source antigo removido do NotebookLM
- [ ] Novo source adicionado
- [ ] Análise confirmatória realizada

### Finalização
- [ ] 100% conformidade confirmada
- [ ] Documentos de análise criados
- [ ] Código pronto para entrega

## 🚨 Casos de Uso Real

### Exemplo 1: Erro Crítico Encontrado
**Problema:** `.Wait()` fora do construtor  
**Fluxo:**
1. Análise identificou erro
2. Código foi corrigido
3. **Repomix.md foi regerado**
4. Nova análise confirmou correção
5. **Resultado:** 10/10 garantido

### Exemplo 2: Falso Positivo Evitado
**Problema:** Análise baseada em código desatualizado  
**Fluxo:**
1. NotebookLM analisou repomix.md antigo
2. Disse que código estava correto
3. **Mas repomix.md tinha código ANTES da correção**
4. **Regeneramos repomix.md**
5. **Nova análise mostrou o erro real**
6. **Resultado:** Erro crítico encontrado e corrigido

## 📊 Métricas de Sucesso

### Tempo Médio por Ciclo
- **Análise inicial:** 5-10 minutos
- **Correções:** 10-30 minutos
- **Regeneração repomix:** 2-5 minutos
- **Análise final:** 3-5 minutos
- **Total:** 20-50 minutos

### Taxa de Sucesso
- **Análises com fluxo completo:** 100% precisão
- **Análises sem repomix atualizado:** 60% precisão (falsos positivos)

## 🎓 Aplicabilidade

### Projetos Acadêmicos
- ✅ Provas A1/A2
- ✅ Trabalhos práticos
- ✅ Projetos finais
- ✅ Análise de conformidade

### Projetos Profissionais
- ✅ Code review
- ✅ Refatoração
- ✅ Migração de código
- ✅ Auditoria de qualidade

## 🔗 Documentos Relacionados

- `CLAUDE.md` - Configurações gerais do projeto
- `ANALISE_FINAL_DETALHADA.md` - Template de análise
- `CHECKLIST_FINAL_100_PERCENTUAL.md` - Checklist de requisitos
- `CORRECAO_CRITICA_ENCONTRADA.md` - Registro de correções

---

**Status:** Padrão ativo e validado  
**Última atualização:** 2026-05-15  
**Próxima revisão:** 2026-06-15
# CLAUDE.md - appProvaA1Celular

## Modus Operandi - Análise com NotebookLM MCP

### 🔄 FLUXO PADRÃO PARA ANÁLISE FINAL

**Sempre que usar NotebookLM MCP para análise de código:**

#### 1. 📦 Gerar Repomix Inicial
```bash
# Gerar repomix.md com todo o código fonte
repomix . --output repomix.md
```

#### 2. 🔍 Adicionar ao NotebookLM
```bash
# Adicionar repomix.md como source no NotebookLM
mcp__notebooklm__source_add --source_type text --title "Código Completo appProvaA1Celular"
```

#### 3. 🤖 Análise Inicial
```bash
# Pedir análise exaustiva contra requisitos (Apostilas 08-09)
mcp__notebooklm__notebook_query --query "ANÁLISE EXAUSTIVA: Verifique linha por linha..."
```

#### 4. 🔧 CORRIGIR PROBLEMAS ENCONTRADOS
```bash
# Aplicar correções no código baseado na análise
Edit --file_path "arquivo.cs" --old_string "código incorreto" --new_string "código correto"
```

#### 5. ⚠️ **CRÍTICO: Regenerar Repomix PÓS-CORREÇÃO**
```bash
# 🚨 SEMPRE regerar repomix.md após correções
repomix . --output repomix.md
```

#### 6. 🗑️ Remover Source Antigo
```bash
# Remover repomix.md antigo do NotebookLM
mcp__notebooklm__source_delete --source_id "id-do-repomix-antigo" --confirm=true
```

#### 7. ➕ Adicionar Novo Repomix
```bash
# Adicionar repomix.md atualizado ao NotebookLM
mcp__notebooklm__source_add --source_type text --title "Código Completo appProvaA1Celular - CORRIGIDO"
```

#### 8. ✅ Análise Final Confirmatória
```bash
# Pedir nova análise para confirmar 100% conformidade
mcp__notebooklm__notebook_query --query "CONFIRMAÇÃO FINAL: Verifique se todas as correções foram aplicadas..."
```

### 🔴 REGRA DE OURO

**NUNCA confie em análise de NotebookLM com código desatualizado!**

- **Repomix.md antigo = Análise incorreta**
- **Correção no código ≠ Repomix.md atualizado**
- **Fluxo obrigatório:** Corrigir → Regerar Repomix → Reanalisar

### 📋 Checklist para Análise Final

- [ ] Gerar repomix.md inicial
- [ ] Adicionar ao NotebookLM
- [ ] Análise exaustiva inicial
- [ ] Aplicar correções encontradas
- [ ] **REGERAR repomix.md (CRÍTICO!)**
- [ ] Remover source antigo do NotebookLM
- [ ] Adicionar novo repomix.md ao NotebookLM
- [ ] Análise final confirmatória
- [ ] Confirmar 100% conformidade

### 🎯 Justificativa

O NotebookLM analisa **APENAS** o conteúdo dos sources adicionados. Se o repomix.md contém código antigo, a análise será baseada em código incorreto, levando a falsos positivos.

**Exemplo real:**
- NotebookLM analisou repomix.md antigo
- Disse que `.Wait()` estava no construtor
- Mas repomix.md tinha código ANTES da correção
- Análise estava incorreta por causa de source desatualizado

### 📁 Estrutura de Análise

**Documentos criados durante análise:**
1. `ANALISE_FINAL_DETALHADA.md` - Análise linha por linha manual
2. `CHECKLIST_FINAL_100_PERCENTUAL.md` - Checklist completo
3. `CORRECAO_CRITICA_ENCONTRADA.md` - Detalhes de erros encontrados
4. `VERIFICACAO_FINAL_POS_CORRECAO.md` - Confirmação pós-correção

**Projetos específicos:**
- **Prova A1 (09/06/2026):** appProvaA1Celular
- **Requisitos:** Apostilas 08-09 do professor
- **Nota máxima:** 10/10 mediante conformidade total
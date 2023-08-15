using XState.Actions;

namespace XState
{
    public interface ResolveTypegenMeta<TTypesMeta, TEvent, TAction, TServiceMap>
        where TTypesMeta : TypegenConstraint
        where TEvent : Event
        where TAction : BaseActionObject
        where TServiceMap : IServiceMap
    {

        /*
         "@@xstate/typegen": TTypesMeta["@@xstate/typegen"];
  resolved: {
    enabled: TTypesMeta & {
      indexedActions: IndexByType<TAction>;
      indexedEvents: MergeWithInternalEvents<
        IndexByType<
          | (string extends TEvent["type"] ? never : TEvent)
          | GenerateServiceEvents<
              TServiceMap,
              Prop<TTypesMeta, "invokeSrcNameMap">
            >
        >,
        Prop<TTypesMeta, "internalEvents">
      >;
    };
        */
        object Resolved { get; }

        /*
    disabled: TypegenDisabled &
      AllImplementationsProvided &
      AllowAllEvents & {
        indexedActions: IndexByType<TAction>;
        indexedEvents: Record<string, TEvent> & {
          __XSTATE_ALLOW_ANY_INVOKE_DATA_HACK__: { data: any };
        };
        invokeSrcNameMap: Record<
          string,
          "__XSTATE_ALLOW_ANY_INVOKE_DATA_HACK__"
        >;
      };
  }[IsNever<TTypesMeta> extends true
    ? "disabled"
    : TTypesMeta extends TypegenEnabled
    ? "enabled"
    : "disabled"];
        */
        object Disabled { get; }
    }
}

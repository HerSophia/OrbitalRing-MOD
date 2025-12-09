using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HarmonyLib;

namespace ProjectOrbitalRing.Patches.Logic
{
    //internal class StorageStackMultiplying
    //{
    //    [HarmonyPatch(typeof(UIControlPanelStationStorage), nameof(UIControlPanelStationStorage.OnItemPickerReturn))]
    //    [HarmonyPrefix]
    //    public static bool OnItemPickerReturn_Patch(UIControlPanelStationStorage __instance, ItemProto itemProto)
    //    {
    //        if (itemProto == null) {
    //            return false;
    //        }
    //        if (!__instance.isStationDataValid || __instance.index >= __instance.station.storage.Length) {
    //            return false;
    //        }
    //        ItemProto itemProto2 = LDB.items.Select((int)__instance.factory.entityPool[__instance.station.entityId].protoId);
    //        if (itemProto2 == null) {
    //            return false;
    //        }
    //        for (int i = 0; i < __instance.station.storage.Length; i++) {
    //            if (__instance.station.storage[i].itemId == itemProto.ID) {
    //                UIRealtimeTip.Popup("不选择重复物品".Translate(), true, 0);
    //                return false;
    //            }
    //        }
    //        int additionStorage = __instance.GetAdditionStorage();

    //        double Multiplying = 1 + ((double)(LDB.items.Select(itemProto.ID).StackSize - 200) / 400.0);
    //        //Multiplying = 2.5;
    //        __instance.masterInspector.transport.SetStationStorage(__instance.station.id, __instance.index, itemProto.ID, (int)((itemProto2.prefabDesc.stationMaxItemCount + additionStorage) * Multiplying), ELogisticStorage.Supply, __instance.station.isStellar ? ELogisticStorage.Supply : ELogisticStorage.None, GameMain.mainPlayer);
    //        return false;
    //    }

    //    [HarmonyPatch(typeof(UIStationStorage), nameof(UIStationStorage.OnItemPickerReturn))]
    //    [HarmonyPrefix]
    //    public static bool OnItemPickerReturnPatch(UIStationStorage __instance, ItemProto itemProto)
    //    {
    //        if (itemProto == null) {
    //            return false;
    //        }
    //        if (__instance.station == null || __instance.index >= __instance.station.storage.Length) {
    //            return false;
    //        }
    //        ItemProto itemProto2 = LDB.items.Select((int)__instance.stationWindow.factory.entityPool[__instance.station.entityId].protoId);
    //        if (itemProto2 == null) {
    //            return false;
    //        }
    //        for (int i = 0; i < __instance.station.storage.Length; i++) {
    //            if (__instance.station.storage[i].itemId == itemProto.ID) {
    //                UIRealtimeTip.Popup("不选择重复物品".Translate(), true, 0);
    //                return false;
    //            }
    //        }
    //        int additionStorage = __instance.GetAdditionStorage();
    //        double Multiplying = 1 + ((double)(LDB.items.Select(itemProto.ID).StackSize - 200) / 400.0);
    //        //Multiplying = 2.5;
    //        __instance.stationWindow.transport.SetStationStorage(__instance.station.id, __instance.index, itemProto.ID, (int)((itemProto2.prefabDesc.stationMaxItemCount + additionStorage) * Multiplying), ELogisticStorage.Supply, __instance.station.isStellar ? ELogisticStorage.Supply : ELogisticStorage.None, GameMain.mainPlayer);
    //        return false;
    //    }

    //    [HarmonyPatch(typeof(PlanetTransport), nameof(PlanetTransport.SetStationStorage))]
    //    [HarmonyPrefix]
    //    public static bool SetStationStorage_Patch(ref PlanetTransport __instance, int stationId, int storageIdx, int itemId, int itemCountMax, ELogisticStorage localLogic, ELogisticStorage remoteLogic, Player player)
    //    {
    //        if (itemId != 0 && LDB.items.Select(itemId) == null) {
    //            itemId = 0;
    //        }
    //        bool flag = false;
    //        bool flag2 = false;
    //        StationComponent stationComponent = __instance.GetStationComponent(stationId);
    //        if (stationComponent != null) {
    //            if (!stationComponent.isStellar) {
    //                remoteLogic = ELogisticStorage.None;
    //            }
    //            if (itemId <= 0) {
    //                itemId = 0;
    //                itemCountMax = 0;
    //                localLogic = ELogisticStorage.None;
    //                remoteLogic = ELogisticStorage.None;
    //            }
    //            int modelIndex = (int)__instance.factory.entityPool[stationComponent.entityId].modelIndex;
    //            ModelProto modelProto = LDB.models.Select(modelIndex);
    //            int num = 0;
    //            if (modelProto != null) {
    //                num = modelProto.prefabDesc.stationMaxItemCount;
    //            }
    //            int num2;
    //            if (stationComponent.isCollector) {
    //                num2 = GameMain.history.localStationExtraStorage;
    //            } else if (stationComponent.isVeinCollector) {
    //                num2 = GameMain.history.localStationExtraStorage;
    //            } else if (stationComponent.isStellar) {
    //                num2 = GameMain.history.remoteStationExtraStorage;
    //            } else {
    //                num2 = GameMain.history.localStationExtraStorage;
    //            }
    //            double Multiplying = 1 + ((double)(LDB.items.Select(itemId).StackSize - 200) / 400.0);
    //            //Multiplying = 2.5;
    //            if (itemCountMax > (int)((num + num2) * Multiplying)) {
    //                itemCountMax = (int)((num + num2) * Multiplying);
    //            }
    //            if (storageIdx >= 0 && storageIdx < stationComponent.storage.Length) {
    //                StationStore stationStore = stationComponent.storage[storageIdx];
    //                if (stationStore.localLogic != localLogic) {
    //                    flag = true;
    //                }
    //                if (stationStore.remoteLogic != remoteLogic) {
    //                    flag2 = true;
    //                }
    //                if (stationStore.itemId == itemId) {
    //                    stationComponent.storage[storageIdx].max = itemCountMax;
    //                    stationComponent.storage[storageIdx].localLogic = localLogic;
    //                    stationComponent.storage[storageIdx].remoteLogic = remoteLogic;
    //                } else {
    //                    if (stationStore.localLogic != ELogisticStorage.None || localLogic != ELogisticStorage.None) {
    //                        flag = true;
    //                    }
    //                    if (stationStore.remoteLogic != ELogisticStorage.None || remoteLogic != ELogisticStorage.None) {
    //                        flag2 = true;
    //                    }
    //                    if (stationStore.count > 0 && stationStore.itemId > 0 && player != null) {
    //                        int num3 = player.TryAddItemToPackage(stationStore.itemId, stationStore.count, stationStore.inc, true, 0, false);
    //                        UIItemup.Up(stationStore.itemId, num3);
    //                        if (num3 < stationStore.count) {
    //                            UIRealtimeTip.Popup("无法收回仓储物品".Translate(), true, 0);
    //                        }
    //                    }
    //                    stationComponent.storage[storageIdx].itemId = itemId;
    //                    stationComponent.storage[storageIdx].count = 0;
    //                    stationComponent.storage[storageIdx].inc = 0;
    //                    stationComponent.storage[storageIdx].localOrder = 0;
    //                    stationComponent.storage[storageIdx].remoteOrder = 0;
    //                    stationComponent.storage[storageIdx].max = itemCountMax;
    //                    stationComponent.storage[storageIdx].localLogic = localLogic;
    //                    stationComponent.storage[storageIdx].remoteLogic = remoteLogic;
    //                }
    //                if (itemId == 0) {
    //                    stationComponent.storage[storageIdx] = default(StationStore);
    //                    for (int i = 0; i < stationComponent.slots.Length; i++) {
    //                        if (stationComponent.slots[i].dir == IODir.Output && stationComponent.slots[i].storageIdx - 1 == storageIdx) {
    //                            stationComponent.slots[i].counter = 0;
    //                            stationComponent.slots[i].storageIdx = 0;
    //                            stationComponent.slots[i].dir = IODir.Output;
    //                        }
    //                    }
    //                }
    //            }
    //            if (!stationComponent.isStellar) {
    //                flag2 = false;
    //            }
    //        }
    //        if (flag) {
    //            __instance.RefreshStationTraffic(stationId);
    //        }
    //        if (flag2) {
    //            __instance.gameData.galacticTransport.RefreshTraffic(stationComponent.gid);
    //        }
    //        return false;
    //    }
    //}
}
